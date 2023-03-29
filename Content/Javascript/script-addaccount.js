//function for name field validation
function Checkacctype() {
    let acctypeinput = document.getElementById("acctype");

    if (acctypeinput.value.trim() === "") {
        setError(acctypeinput, "Empty type");
    }
    else {
        setSuccess(acctypeinput);
    }
}

function Checkbranchid() {
    let branchidinput = document.getElementById("branchid");

    if (branchidinput.value.trim() === "") {
        setError(branchidinput, "Empty branch");
    }
    else {
        setSuccess(branchidinput);
    }
}

function Checkfile() {
    let fileinput = document.getElementById("file");

    if (fileinput.value.trim() === "") {
        setError(fileinput, "Empty file");
    }
    else {
        setSuccess(fileinput);
    }
}

//till here
function Checkvalidation() {
    Checkacctype();
    Checkbranchid();
    Checkfile();
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