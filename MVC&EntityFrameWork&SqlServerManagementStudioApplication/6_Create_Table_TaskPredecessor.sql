USE TaskManagement
CREATE TABLE TaskPredecessor 
(
	TaskId int,
	PredecessorId int,
	FOREIGN KEY (TaskId) REFERENCES TaskManagement.dbo.Task(TaskId) ON DELETE CASCADE,
	FOREIGN KEY (PredecessorId) REFERENCES TaskManagement.dbo.Task(TaskId)
);