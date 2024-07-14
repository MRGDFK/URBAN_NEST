'use strict';

const $navbar = document.querySelector("[data-navbar]");
const $navToggler = document.querySelector("[data-nav-toggler]");

$navToggler.addEventListener("click", () => $navbar.classList.toggle("active"));

/** header scroll */
const $header = document.querySelector("[data-header");

window.addEventListener("scroll", e => {
    $header.classList[window.scrollY > 50 ? "add" : "remove"]("active");
});

/* favourite toogle */
const $favToggler = document.querySelectorAll("[data-fav-toggle-btn]");
$favToggler.forEach($toggleBtn => {
    $toggleBtn.addEventListener("click", () => {
        $toggleBtn.classList.toggle("active");
    });
});

document.getElementById('accountBtn').addEventListener('click', function () {
    document.getElementById('accountDetails').style.display = 'block';
});
