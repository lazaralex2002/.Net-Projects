<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForms._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
      <div class="dropdown">
      <div class="dropbtn">File</div>
      <div class="dropdown-content">
        <a onclick="InitializeButtonClicked()">Initialize</a>
        <a onclick="SaveButtonClicked()">Save</a>
        <a onclick="OpenButtonClicked()">Open</a>
        <a onclick="QuitButtonClicked()">Quit</a>
      </div>
    </div>

    <table id="table" style="width:100%" class="table table-bordered table-hover" >
        <thead class="thead-dark " >
            <tr>
                <th scope="col">TaskID</th>
                <th scope="col">TaskName</th>
                <th scope="col">Duration</th>
                <th scope="col">Start</th>
                <th scope="col">Finish</th>
                <th scope="col">Predecessors</th>
                <th scope="col">ResourceNames</th>
                <th scope="col">TaskMode</th>
            </tr>
        </thead>
        <tbody id ="tableBody">
            
        </tbody>
</table>
<input id="fileDialog" type="file" style="display:none" onchange="ShowFile(this)" accept=".xml"/>
<a id="saveFileDialog" href="data:application/xml;charset=utf-8,your code here" download="project.xml" style="display:none">Save</a>

<script>
    function InitializeButtonClicked()
    {
        PageMethods.Initialize(ProjectInitialized, OnError);
    }

    function OpenButtonClicked()
    {
        document.getElementById('fileDialog').click();//if a file is provided the ShowFile function is called     
    }

    function QuitButtonClicked()
    {
        PageMethods.Quit(function (result) { }, OnError);
        ReplaceBody();
    }

    function SaveButtonClicked()
    {
        PageMethods.Serialize(SerializeSuccess, OnError);
    }

    function ShowFile(input)
    {
        ReplaceBody();
        let file = input.files[0];

        let reader = new FileReader();
        reader.readAsText(file);
        reader.onload = function ()
        {
            console.log(reader.result);
            PageMethods.Deserialize(reader.result,
                function (result)
                {
                    PageMethods.GetTasks(GetTasksSuccess, OnError);
                }
                , OnError);
        };

        reader.OnError = function () {
            console.log(reader.error);
        };
    }

    function ProjectInitialized(result)
    {
        ReplaceBody();
        PageMethods.GetTasks(GetTasksSuccess, OnError);
    }

    function SerializeSuccess(result)
    {
        let a = document.getElementById("saveFileDialog");
        let href = "data: application/xml;charset=utf-8," + result;
        a.href = href;
        a.click();
    }

    function AddCellContentEditedEventListner()
    {
        let textBefore;

        var row = document.getElementById('table').rows;
        for (var i = 0; i < row.length; i++) //adding an event listener for focus and lostFocus, for each cell of the table 
        {
            for (var j = 0; j < row[i].cells.length; j++)
            {
                row[i].cells[j].addEventListener('focusin', (event) => {
                    textBefore = event.target.innerText;
                });
                row[i].cells[j].addEventListener('focusout', (event) =>
                {
                    if (event.target.innerText != textBefore) //doing this here so both fuctions have access to the variable textbefore
                    {
                        var textBeforePageMethod = textBefore;
                        PageMethods.UpdateField($('table tr').index(event.target.parentElement), event.target.cellIndex, event.target.innerText, 
                            function (response)
                            {
                                console.toString()
                                if (response.toString() === "false")
                                {
                                    event.target.innerText = textBeforePageMethod;
                                    alert("invalid syntax");
                                }
                            }
                            , OnError);
                    }
                });
            }
        }
    }

    function GetTasksSuccess(result)
    {
        console.log(result.toString());
        var projectObject = JSON.parse(result);

        projectObject.forEach(obj => {
            obj.TaskID
            $("table").find('tbody').append("<tr>" +
                "<td>" + obj.TaskID + "</td>" +
                "<td contenteditable=" + "true" + ">" + obj.TaskName + "</td>" +
                "<td contenteditable=" + "true" + ">" + obj.Duration + "</td>" +
                "<td contenteditable=" + "true" + ">" + obj.Start + "</td>" +
                "<td contenteditable=" + "true" + ">" + obj.Finish + "</td>" +
                "<td>" + obj.Predecessors + "</td>" +
                "<td>" + obj.ResourceNames + "</td>" +
                "<td>" + obj.TaskMode + "</td>" +
                + "</tr>");
        });
        AddCellContentEditedEventListner();
    }

    function OnError(result)
    {
        alert(result.get_message());
    }

    function ReplaceBody()
    {
        document.getElementById('tableBody').innerText = "";
    }
</script>
</asp:Content>
