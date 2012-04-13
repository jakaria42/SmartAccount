﻿using System.Collections.Generic;
using BLL.Model;
using BLL.Model.Managers;
using BLL.Model.Repositories;
using BLL.Model.Schema;
using System.Linq;
using System;

namespace BLL.ProjectManagement
{
    public class ProjectManager : ManagerBase, IProjectManager
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IHeadRepository _headRepository;
        private readonly IRecordRepository _recordRepository;

        public ProjectManager(IProjectRepository projectRepository, IHeadRepository headRepository, IRecordRepository recordRepository)
        {
            _projectRepository = projectRepository;
            _headRepository = headRepository;
            _recordRepository = recordRepository;
        }

        public IList<Project> GetProjects(bool bringInactive = true)
        {
            if (bringInactive)
                return _projectRepository.GetAll().OrderBy(p => p.Name).ToList();
            return _projectRepository.GetAll().Where(p => p.IsActive).OrderBy(p => p.Name).ToList();
        }

        public bool Add(Project project)
        {
            Project existingProject = _projectRepository.Get(project.Name);

            if (existingProject != null)
            {
                //_message = MessageService.Instance.Get("ProjectAlreadyExists", MessageType.Error);
                //_message.MessageText = string.Format(_message.MessageText, project.Name);
                InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Error, MessageKey = "ProjectAlreadyExists", Parameters = new Dictionary<string, string> { { "ProjectName", project.Name } } });
                return false;
            }

            Project insertedProject = _projectRepository.Insert(project);
            if (insertedProject != null)
            {
                int cashBookId = _headRepository.Get("Cash Book").Id;
                int bankBookId = _headRepository.Get("Bank Book").Id;
                //int advanceId = _headRepository.Get("Advance").Id;
                AddHeadsToProject(insertedProject.Id, new int[] { cashBookId, bankBookId });

                //_message = MessageService.Instance.Get("NewProjectSuccessfullyCreated", MessageType.Success);
                //_message.MessageText = string.Format(_message.MessageText, insertedProject.Name);
                InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Success, MessageKey = "NewProjectSuccessfullyCreated", Parameters = new Dictionary<string, string> { { "ProjectName", insertedProject.Name } } });

                if (_headRepository.Get(project.Name) == null)
                {
                    Head newHead = new Head
                                       {
                                           Name = insertedProject.Name,
                                           IsActive = true,
                                           Type = HeadType.Capital,
                                           Description =
                                               "This head (related with project '" + project.Name +
                                               "') is only for inter project loan."
                                       };
                    Head insertedHead = _headRepository.Insert(newHead);
                    if (insertedHead != null)
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
            Project existingProject = _projectRepository.Get(project.Name);

            if (existingProject != null)
            {
                _projectRepository.Update(project);
                //_message = MessageService.Instance.Get("ProjectSuccessfullyUpdated", MessageType.Success);
                //_message.MessageText = string.Format(_message.MessageText, existingProject.Name);
                InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Success, MessageKey = "ProjectSuccessfullyUpdated", Parameters = new Dictionary<string, string> { { "ProjectName", existingProject.Name } } });
                return true;
            }
            //_message = MessageService.Instance.Get("ProjectUpdatedFailed", MessageType.Error);
            //_message.MessageText = string.Format(_message.MessageText, project.Name);
            InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Error, MessageKey = "ProjectUpdatedFailed", Parameters = new Dictionary<string, string> { { "ProjectName", project.Name } } });
            return false;
        }

        public int RemoveHeadsFromProject(int projectId, int[] headIds)
        {
            int count = 0;
            Project project = _projectRepository.Get(projectId);

            if (project == null)
            {
                //_message.MessageText = "Invalid project selected.";
                //_message.MessageType = MessageType.Warning;
                InvokeManagerEvent(new BLLEventArgs { EventType = EventType.Warning, MessageDescription = "Invalid project selected." });

                return 0;
            }

            string headNames = "";
            Head head;
            foreach (int headId in headIds)
            {
                head = _headRepository.Get(headId);
                if (head != null)
                {
                    if (_projectRepository.RemoveHeadFromProject(projectId, headId))
                    {
                        headNames += string.IsNullOrWhiteSpace(headNames) ? head.Name : ", " + head.Name;
                        count++;
                    }
                }
            }

            if (count > 0)
            {
                InvokeManagerEvent(new BLLEventArgs
                {
                    EventType = EventType.Success,
                    MessageDescription = count + " head(s) removed from project '" + project.Name + "': " +
                        headNames + "."
                });

            }
            else
            {
                InvokeManagerEvent(new BLLEventArgs
                {
                    EventType = EventType.Information,
                    MessageDescription = "No head(s) removed from project '" + project.Name + "'."
                });
            }
            return count;
        }

        public int AddHeadsToProject(int projectId, int[] headIds)
        {
            int count = 0;
            Project project = _projectRepository.Get(projectId);

            if (project == null)
            {
                InvokeManagerEvent(new BLLEventArgs
                {
                    EventType = EventType.Warning,
                    MessageDescription = "Invalid project selected."
                });
                return 0;
            }

            string headNames = "";
            Head head;
            foreach (int headId in headIds)
            {
                head = _headRepository.Get(headId);
                if (head != null)
                {
                    if (_projectRepository.AddHeadToProject(projectId, headId))
                    {
                        headNames += string.IsNullOrWhiteSpace(headNames) ? head.Name : ", " + head.Name;
                        count++;
                    }
                }
            }

            if (count > 0)
            {
                InvokeManagerEvent(new BLLEventArgs
                {
                    EventType = EventType.Success,
                    MessageDescription = count + " head(s) added to project '" + project.Name + "': " + headNames + "."
                });
            }
            else
            {
                InvokeManagerEvent(new BLLEventArgs
                {
                    EventType = EventType.Information,
                    MessageDescription = "No head(s) added to project '" + project.Name + "'."
                });
            }
            return count;
        }

        public bool IsRecordFound(int projectId, int headId)
        {
            return _recordRepository.IsRecordFound(projectId, headId);
        }
    }
}

