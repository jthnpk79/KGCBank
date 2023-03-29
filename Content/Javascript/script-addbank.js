//function for name field validation
function Checkname() {
    let nameinput = document.getElementById("name");

    if (nameinput.value.trim() === "") {
        setError(nameinput, "Empty name");
    }
    else {
        setSuccess(nameinput);
    }
}

function Checkdescription() {
    let descriptioninput = document.getElementById("description");

    if (descriptioninput.value.trim() === "") {
        setError(descriptioninput, "Empty description");
    }
    else {
        setSuccess(descriptioninput);
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

function Checkphone() {
    let phoneinput = document.getElementById("phone");
    if (phoneinput.value.trim() === "") {
        setError(phoneinput, "Empty phone number");
    }
    else {
        setSuccess(phoneinput);
    }
}

//till here
function Checkvalidation() {
    Checkname();
    Checkdescription();
    Checkifsc();
    Checkphone();
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