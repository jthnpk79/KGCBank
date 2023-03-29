//function for name field validation
function Checkaccnumber() {
    let numberinput = document.getElementById("AccNumber");

    if (numberinput.value.trim() === "") {
        setError(numberinput, "Empty account number");
    }
    else {
        setSuccess(numberinput);
    }
}

function Checkacctype() {
    let typeinput = document.getElementById("AccType");
    if (typeinput.value.trim() === "") {
        setError(typeinput, "Empty account type");
    }
    else {
        setSuccess(typeinput);
    }
}

function Checkstatus() {
    let statusinput = document.getElementById("Status");
    if (statusinput.value.trim() === "") {
        setError(statusinput, "Empty account status");
    }
    else {
        setSuccess(statusinput);
    }
}


//till here
function Checkvalidation() {
    Checkaccnumber();
    Checkacctype();
    Checkstatus();
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