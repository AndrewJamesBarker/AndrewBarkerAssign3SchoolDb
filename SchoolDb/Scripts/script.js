window.onload = function() {



    var formHandle = document.forms.formTeacher;

    formHandle.onsubmit = formValidate;

    function formValidate() {

        var fNameValue = formHandle.TeacherfName;
        var lNameValue = formHandle.TeacherlName;
        var employNumValue = formHandle.EmployeeNumber;
        var salaryValue = formHandle.Salary;
        var hireDateValue = formHandle.HireDate;
    /*    var hireDateRegex = ^\d{ 4} \-(0 ? [1 - 9]| 1[012]) \-(0 ? [1 - 9] | [12][0 - 9] | 3[01])$;*/


        if (fNameValue.value === "" && lNameValue.value === "" && employNumValue.value === "" && salaryValue.value === "" && hireDateValue.value === "") {
            alert("enter all fields!");
            return false;
        }

        if (fNameValue.value === "") {
            fNameValue.style["background-color"] = "red";
            fNameValue.focus();
            return false;
        }
        if (lNameValue.value === "") {
            lNameValue.style["background-color"] = "red";
            lNameValue.focus();
            return false;
        }
        if (employNumValue.value === "") {
            employNumValue.style["background-color"] = "red";
            employNumValue.focus();
            return false;
        }
        if (salaryValue.value === "") {
            salaryValue.style["background-color"] = "red";
            salaryValue.focus();
            return false;
        }
        if (!hireDateRegex.test(hireDateValue.value)) {
            hireDateValue.style["background-color"] = "red";
            hireDateValue.focus();
            return false;
        }
        return true;


        //function formValidate() {

        //    var fNameValue = document.getElementById("TeacherFname");
        //    var lNameValue = document.getElementById("TeacherLname");
        //    var employNumValue = document.getElementById("EmployeeNumber");
        //    var salaryValue = document.getElementById("Salary");
        //    var hireDateValue = document.getElementById("HireDate");
        //    var hireDateRegex = ^\d{ 4 } \-(0 ? [1 - 9] | 1[012]) \-(0 ? [1 - 9] | [12][0 - 9] | 3[01])$;


        //    if (fNameValue.value === "" && lNameValue.value === "" && employNumValue.value === "" && salaryValue.value === "" && hireDateValue.value === "") {
        //        alert("enter all fields!");
        //        return false;
        //    }

        //    if (fNameValue.value === "") {
        //        fNameValue.style["background-color"] = "red";
        //        fNameValue.focus();
        //        return false;
        //    }
        //    if (lNameValue.value === "") {
        //        lNameValue.style["background-color"] = "red";
        //        lNameValue.focus();
        //        return false;
        //    }
        //    if (employNum.value === "") {
        //        employNumValue.style["background-color"] = "red";
        //        employNumValue.focus();
        //        return false;
        //    }
        //    if (salaryValue.value === "") {
        //        salaryValue.style["background-color"] = "red";
        //        salaryValue.focus();
        //        return false;
        //    }
        //    if (!hireDateRegex.test(hireDateValue.value)) {
        //        hireDateValue.style["background-color"] = "red";
        //        hireDateValue.focus();
        //        return false;
        //    }
        //    return true;
    }

}
