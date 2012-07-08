﻿using System.Collections.Generic;
using BLL.Model;
using BLL.Model.Entity;
using BLL.Model.Managers;
using BLL.Model.Repositories;
using System.Linq;
using System;

namespace BLL.ProjectManagement
{
    public class ProjectManager : ManagerBase, IProjectManager
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<ProjectHead> _projectHeadRepository;
        private readonly IRepository<Head> _headRepository;
        private readonly IRepository<Record> _recordRepository;
        private readonly IRepository<OpeningBalance> _openingBalanceRepository;
        private readonly IRepository<Parameter> _parameterRepository;

        private readonly string currentFinancialYear;

        public ProjectManager(IRepository<Project> projectRepository, IRepository<Head> headRepository, IRepository<ProjectHead> projectHeadRepository, IRepository<Record> recordRepository, IRepository<OpeningBalance> openingBalanceRepository, IRepository<Parameter> parameterRepository)
        {
            _projectRepository = projectRepository;
            _headRepository = headRepository;
            _projectHeadRepository = projectHeadRepository;
            _recordRepository = recordRepository;
            _openingBalanceRepository = openingBalanceRepository;
            _parameterRepository = parameterRepository;

            //currentFinancialYear = _parameterRepository.GetSingle(p => p.Key == "CurrentFinancialYear").Value;
            currentFinancialYear = "2012";
        }

        public IList<Project> GetProjects(bool bringInactive = true)
        {
            if (bringInactive)
                return _projectRepository.GetAll().OrderBy(p => p.Name).ToList();
            return _projectRepository.GetAll().Where(p => p.IsActive).OrderBy(p => p.Name).ToList();
        }

        public bool Add(Project project)
        {
            if (ProjectWithSameNameAlreadyExists(project)) return false;

            _projectRepository.Insert(project);
            Head cashBook = _headRepository.GetSingle(h => h.Name == "Cash Book");
            Head bankBook = _headRepository.GetSingle(h => h.Name == "Bank Book");

            _projectHeadRepository.Insert(new ProjectHead() { Project = project, Head = cashBook, IsActive = true });
            _projectHeadRepository.Insert(new ProjectHead() { Project = project, Head = bankBook, IsActive = true });

            if (_projectRepository.Save() > 0)
            {
                InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Success, MessageKey = "NewProjectSuccessfullyCreated", Parameters = new Dictionary<string, string> { { "ProjectName", project.Name } } });

                if (_headRepository.GetSingle(h => h.Name == project.Name) == null)
                {
                    Head newHead = new Head
                                       {
                                           Name = project.Name,
                                           IsActive = true,
                                           HeadType = "Capital",
                                           Description =
                                               "This head (related with project '" + project.Name +
                                               "') is only for inter project loan."
                                       };
                    _headRepository.Insert(newHead);
                    if (_headRepository.Save() > 0)
                        InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Success, MessageDescription = "A head with name '" + project.Name + "' is created for inter project loan." });
                }
                else
                {
                    InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Success, MessageDescription = "Same head with name '" + project.Name + "' already found." });
                }
                return true;
            }
            return false;
        }

        public bool Update(Project project)
        {
            //if (!ProjectWithSameNameAlreadyExists(project)) return false;
            _projectRepository.Update(project);
            if (_projectRepository.Save() > 0)
            {
                InvokeManagerEvent(new BLLEventArgs
                                       {
                                           EventType = EventType.Success,
                                           MessageKey = "ProjectSuccessfullyUpdated",
                                           Parameters = new Dictionary<string, string> { { "ProjectName", project.Name } }
                                       });
                return true;
            }
            InvokeManagerEvent(new BLLEventArgs
                                   {
                                       EventType = EventType.Error,
                                       MessageKey = "ProjectUpdatedFailed",
                                       Parameters = new Dictionary<string, string> { { "ProjectName", project.Name } }
                                   });
            _projectRepository.Discard();
            return false;
        }

        public int RemoveHeadsFromProject(Project project, IList<Head> heads)
        {
            int count = 0;
            string headNames = "";

            foreach (Head deletableHead in heads)
            {                
                ProjectHead deletableProjectHead = _projectHeadRepository.GetSingle(ph => ph.Project.ID == project.ID && ph.Head.ID == deletableHead.ID);
                if (deletableProjectHead != null)
                    headNames += string.IsNullOrWhiteSpace(headNames) ? deletableHead.Name : ", " + deletableHead.Name;
                project.ProjectHeads.Remove(deletableProjectHead);
            }
            count = _projectHeadRepository.Save();

            if (count > 0)
            {
                InvokeManagerEvent(EventType.Success, "", string.Concat("Head(s) removed from project '", project.Name, "': ", headNames, "."));
            }
            else
            {
                InvokeManagerEvent(EventType.Information, "", string.Concat("No head(s) removed from project '", project.Name, "'."));
            }
            return count;
        }

        public int AddHeadsToProject(Project project, IList<Head> heads)
        {
            int count = 0;
            string headNames = "";
            foreach (Head addableHead in heads)
            {
                ProjectHead projectHead = _projectHeadRepository.GetSingle(ph => ph.Project.ID == project.ID && ph.Head.ID == addableHead.ID);
                if (projectHead == null)
                {
                    ProjectHead newProjectHead = new ProjectHead { Project = project, Head = addableHead, IsActive = true };
                    _projectHeadRepository.Insert(newProjectHead);

                    if (addableHead.HeadType == "Capital")
                    {
                        OpeningBalance openingBalance = new OpeningBalance
                        {
                            Balance = 0,
                            FinancialYear = currentFinancialYear,
                            Date = DateTime.Today,
                            IsActive = Convert.ToInt32(currentFinancialYear) < DateTime.Now.Year ? false : true,
                            Description = "opening",
                            ProjectHead = newProjectHead
                        };
                        _openingBalanceRepository.Insert(openingBalance);
                    }

                    headNames += string.IsNullOrWhiteSpace(headNames) ? addableHead.Name : ", " + addableHead.Name;
                }
            }

            count = _projectHeadRepository.Save();

            if (count > 0)
            {
                _openingBalanceRepository.Save();
                InvokeManagerEvent(EventType.Success, "", string.Concat("Head(s) added to project '", project.Name, "': ", headNames, "."));
            }
            else
            {
                _projectHeadRepository.Discard();
                InvokeManagerEvent(EventType.Information, string.Concat("No head(s) added to project '", project.Name, "'."));
            }
            return count;
        }

        public bool IsRecordFound(Project project, Head head)
        {
            if (project == null || head == null) return false;
            ProjectHead projectHead = _projectHeadRepository.GetSingle(ph => ph.Project.ID == project.ID && ph.Head.ID == head.ID);
            if (projectHead == null) return false;
            return _recordRepository.Get(r => r.ProjectHead.ID == projectHead.ID).Count() > 0;
        }

        private bool ProjectWithSameNameAlreadyExists(Project project)
        {
            Project existingProject = _projectRepository.GetSingle(p => p.Name == project.Name);
            if (existingProject != null) InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Error, MessageKey = "ProjectAlreadyExists", Parameters = new Dictionary<string, string> { { "ProjectName", project.Name } } });
            return existingProject != null;
        }
    }
}


