// connect to the project via Shared/_Layout.cshtml


function UpdateTeacher() {



    //goal: send a request like this:
    //POST : http://localhost:50226/api/TeacherData/UpdateTeacher/
    //with post data of TeacherFname, TeacherLname, EmployeeNumber, Salary, HireDate

    var URL = "http://localhost:50226/api/TeacherData/UpdateTeacher/";

    var rq = new XMLHttpRequest();
    //where is the request sent?
    //is it GET or Post?
    //what should be done with the response?

    var TeacherFname = document.getElementById('TeacherFname').value;
    var TeacherLname = document.getElementById('TeacherLname').value;
    var EmployeeNumber = document.getElementById('EmployeeNumber').value;
    var Salary = document.getElementById('Salary').value;
    var HireDate = document.getElementById('HireDate').value;

    var TeacherData = {
        "TeacherFname": TeacherFname,
        "TeacherLname": TeacherLname,
        "EmployeeNumber": EmployeeNumber,
        "Salary": Salary,
        "HireDate": HireDate
    };

    rq.open("GET", URL, true);
    rq.setRequestHeader("Content-Type", "application/json");
    rq.onreadystatechange = function () {
        //ready state should be 4 and status should be 200
        if (rq.readyState == 4 && rq.status == 200) {
            // successful, nothing to render
            console.log(rq.responseText);
        }
    }

    // POST information sent through the .send() method
    rq.send(JSON.stringify(TeacherData));


}