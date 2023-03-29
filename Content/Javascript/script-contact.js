//function for name field validation
function Checkname() {
    let nameinput = document.getElementById("Name");

    if (nameinput.value.trim() === "") {
        setError(nameinput, "Empty name");
    }
    else {
        setSuccess(nameinput);
    }
}

function Checkemail() {
    let pwdinput = document.getElementById("Email");
    if (pwdinput.value.trim() === "") {
        setError(pwdinput, "Empty email");
    }
    else {
        setSuccess(pwdinput);
    }
}

function Checkmessage() {
    let pwdinput = document.getElementById("Message");
    if (pwdinput.value.trim() === "") {
        setError(pwdinput, "Empty message");
    }
    else {
        setSuccess(pwdinput);
    }
}

function setError(input, message) {
    const formControl = input.parentElement;
    const small = formControl.querySelector('small');
    small.className = "smallshown";
    small.innerText = message;
    submitbutton.disabled = true;
}

function setSuccess(input) {
    const formControl = input.parentElement;
    const small = formControl.querySelector('small');
    small.className = "smallhidden";
    small.innerHTML = "";
    submitbutton.disabled = false;
}

//till here
function Checkvalidation() {
    Checkname();
    Checkemail();
    Checkmessage();
}