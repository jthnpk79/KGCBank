//function for name field validation
function Checkaccnumber() {
    let accnumberinput = document.getElementById("accnumber");

    if (accnumberinput.value.trim() === "") {
        setError(accnumberinput, "Empty number");
    }
    else {
        setSuccess(accnumberinput);
    }
}

function Checkpassword() {
    let passwordinput = document.getElementById("password");

    if (passwordinput.value.trim() === "") {
        setError(passwordinput, "Empty password");
    }
    else {
        setSuccess(passwordinput);
    }
}

function Checkamount() {
    var isAmount = /^[0-9]+$/;
    let amountinput = document.getElementById("amount");

    if (amountinput.value.trim() === "") {
        setError(amountinput, "Empty amount");
    }
    else if (amountinput.value.trim() === "0") {
        setError(amountinput, 'Enter valid amount');
    }
    else if (!isAmount.test(amountinput.value.trim())){
        setError(amountinput, 'Enter valid amount');
    }
    else {
        setSuccess(amountinput);
    }
}

//till here
function Checkvalidation() {
    Checkaccnumber();
    Checkpassword();
    Checkamount();
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