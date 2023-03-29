//function for name field validation
function Checkstartdate() {
    let startdateinput = document.getElementById("startdate");

    if (startdateinput.value.trim() === "") {
        setError(startdateinput, "Empty date");
    }
    else {
        setSuccess(startdateinput);
    }
}

function Checkenddate() {
    let enddateinput = document.getElementById("enddate");

    if (enddateinput.value.trim() === "") {
        setError(enddateinput, "Empty date");
    }
    else {
        setSuccess(enddateinput);
    }
}

//till here
function Checkvalidation() {
    Checkstartdate();
    Checkenddate();
}

function setError(input, message) {
    let submitbutton = document.getElementById("button")
    const formControl = input.parentElement;
    const small = formControl.querySelector('small');
    small.className = "smallshown";
    small.innerText = message;
    submitbutton.disabled = true;
}

function setSuccess(input) {
    let submitbutton = document.getElementById("button")
    const formControl = input.parentElement;
    const small = formControl.querySelector('small');
    small.className = "smallhidden";
    small.innerHTML = "";
    submitbutton.disabled = false;
}