// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

var movieEntries = document.getElementsByClassName("movie-entry");

var form = document.getElementById("search-and-filter-form");

form.addEventListener("submit", function (event) {
    event.preventDefault();
    var i, entry;

    var searchTerm = document.getElementById("search").value;

    var mpaa = [];
    var mpaaCheckBoxes = document.getElementsByName("mpaa");
    for (var j = 0; j < mpaaCheckBoxes.length; j++) {
        if (mpaaCheckBoxes[j].checked) {
            mpaa.push(mpaaCheckBoxes[j].value);
        }
    }
    var minIMDB = document.getElementById("minIMDB").value;

    for (i = 0; i < movieEntries.length; i++) {
        entry = movieEntries[i];
        entry.style.display = "block";

        if (searchTerm && !entry.dataset.title.toLowerCase().includes(searchTerm.toLowerCase())) {
            entry.style.display = "none";
        }

        if (mpaa.length > 0 && !mpaa.includes(entry.dataset.mpaa)) {
            entry.style.display = "none";
        }

        if (minIMDB) {
            if (!entry.dataset.imdb || parseFloat(minIMDB) > parseFloat(entry.dataset.imdb)) {
                entry.style.display = "none";
            }
        }
    }
});