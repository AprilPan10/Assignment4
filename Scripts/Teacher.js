// AJAX for teacher Add can go in here!
// This file is connected to the project via Shared/_Layout.cshtml


function AddTeacher() {

	//goal: send a request which looks like this:
	//POST : http://localhost:50354/api/TeacherData/AddTeacher
	//with POST data of teacher first name, teacher last name

	var URL = "http://localhost:50354/api/TeacherData/AddTeacher";

	var rq = new XMLHttpRequest();
	// where is this request sent to?
	// is the method GET or POST?
	// what should we do with the response?

	



	var TeacherData = {
		"TeacherFname": TeacherFname,
		"TeacherLname": TeacherLname,
		"TeacherEmployeenumber": TeacherEmployeenumber,
		"TeacherHiredate": TeacherHiredate,
		"TeacherSalary": TeacherSalary
	};
	var formHandle = document.forms.form_contact;
	formHandle.onsubmit = processForm;
	function processForm() {
		var TeacherFname = document.getElementById('TeacherFname').value;
		var TeacherLname = document.getElementById('TeacherLname').value;
		var TeacherEmployeenumber = document.getElementById('TeacherEmployeenumber').value;
		var TeacherHiredate = document.getElementById('TeacherHiredate').value;
		var TeacherSalary = document.getElementById('TeacherSalary').value;

		if (TeacherFname === "" || TeacherFname === null) {
			nameMsg = document.getElementById('TeacherFname');
			nameMsg.style.backgroundColor = "#FED5B3";
			nameMsg.focus();
			return false;
		} 
		if (TeacherLname === "" || TeacherLname === null) {
			nameMsg = document.getElementById('TeacherLname');
			nameMsg.style.backgroundColor = "#FED5B3";
			nameMsg.focus();
			return false;
		}
	}

	
	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

			//nothing to render, the method returns nothing.


		}

	}
	//POST information sent through the .send() method
	rq.send(JSON.stringify(TeacherData));

}