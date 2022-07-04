CREATE PROCEDURE GetProjectCost
AS
BEGIN
select p.ProjectId, p.ProjectName, SUM(t.Duration * r.ResourceRate) as ProjectCost from 
Project p join Task t on p.ProjectId = t.ProjectId 
join ResourceTask rt on rt.TaskId = t.TaskId 
join Resource r on rt.ResourceId = r.ResourceId 
group by p.ProjectId, p.ProjectName;
END;