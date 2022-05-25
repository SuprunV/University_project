// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.querySelectorAll(".dropdown-toggle").forEach(item => {
    item.addEventListener("click", function () {
        let btnId = this.getAttribute("id");
        let close = this.getAttribute("aria-expanded");
        let list = document.querySelector(`[aria-mylabelledby='${btnId}']`);
        if (list.classList.contains("show")) list.classList.remove("show");
        else list.classList.add("show");
        //else list.classList.add("show");
    })
});