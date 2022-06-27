USE TaskManagement
CREATE TABLE ResourceTask
(
	ResourceId int,
	TaskId int,
	FOREIGN KEY (ResourceId) REFERENCES TaskManagement.dbo.Resource(ResourceId) ON DELETE CASCADE,
	FOREIGN KEY (TaskId) REFERENCES TaskManagement.dbo.Task(TaskId) ON DELETE CASCADE
);