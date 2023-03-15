function allFunction(id) {
    closeAllDropDowns();

    var elemName = "allDropDown" + id;
    var elem = document.getElementById(elemName);
    elem.classList.toggle("show");
}

function processingFunction(id) {
    closeAllDropDowns();

    var elemName = "processingDropDown" + id;
    var elem = document.getElementById(elemName);
    elem.classList.toggle("show");
}

function completedFunction(id) {
    closeAllDropDowns();

    var elemName = "completedDropDown" + id;
    var elem = document.getElementById(elemName);
    elem.classList.toggle("show");
}
  
// Close the dropdown if the user clicks outside of it
window.onclick = function (event) {
    if (!event.target.matches('.dropbtn')) {
        closeAllDropDowns();
    }
}

function closeAllDropDowns() {
    var dropdowns = document.getElementsByClassName("dropdown-content");
    var i;
    for (i = 0; i < dropdowns.length; i++) {
        var openDropdown = dropdowns[i];
        if (openDropdown.classList.contains('show')) {
            openDropdown.classList.remove('show');
        }
    }
}