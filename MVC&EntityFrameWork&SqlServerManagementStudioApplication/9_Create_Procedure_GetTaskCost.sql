CREATE PROCEDURE GetTaskCost
AS
BEGIN
select p.ProjectId, t.TaskId, SUM(Duration * r.ResourceRate) as TaskCost from 
Project p join Task t on p.ProjectId = t.ProjectId 
join ResourceTask rt on rt.TaskId = t.TaskId 
join Resource r on rt.ResourceId = r.ResourceId
group By p.ProjectId, t.TaskId
END;