// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var models = document.getElementsByClassName("idModels");
if (models != null) {
    if (models[0] != null) {
        models[0].checked = true;
    }
}

var labels = document.querySelectorAll(".buttonForCheckboxHidden");
var numeric = document.getElementsByClassName("productCount");
for (i = 0; i < labels.length; i++) {
    labels[i].addEventListener('click', function (i) {
        if (labels[i].innerHTML == "Добавить") {
            labels[i].innerHTML = "Убрать";
            numeric[i].removeAttribute("disabled");
        }

        else {
            labels[i].innerHTML = "Добавить";
            numeric[i].setAttribute('disabled', 'disabled');
        }
            

        //enable checkbox
    }.bind(this, i))
    

    
}