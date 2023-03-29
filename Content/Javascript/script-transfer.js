//function for name field validation
function Checkaccnumber() {
    let numberinput = document.getElementById("accnumber");

    if (numberinput.value.trim() === "") {
        setError(numberinput, "Empty account number");
    }
    else {
        setSuccess(numberinput);
    }
}

function Checkifsc() {
    let ifscinput = document.getElementById("ifsc");

    if (ifscinput.value.trim() === "") {
        setError(ifscinput, "Empty ifsc");
    }
    else {
        setSuccess(ifscinput);
    }
}

function Checkaccholder() {
    let accholderinput = document.getElementById("accholder");

    if (accholderinput.value.trim() === "") {
        setError(accholderinput, "Empty name");
    }
    else {
        setSuccess(accholderinput);
    }
}

function Checkamount() {
    let amountinput = document.getElementById("amount");
    if (amountinput.value.trim() === "") {
        setError(amountinput, "Empty amount");
    }
    else {
        setSuccess(amountinput);
    }
}

//till here
function Checkvalidation() {
    Checkaccnumber();
    Checkaccname();
    Checkifsc();
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