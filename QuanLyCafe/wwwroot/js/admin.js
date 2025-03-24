document.addEventListener("DOMContentLoaded", function () {
    const sidebar = document.getElementById("sidebar");
    const mainContent = document.querySelector("main");

    document.getElementById("sidebarToggle").addEventListener("click", function () {
        sidebar.classList.toggle("collapsed");
        mainContent.classList.toggle("collapsed");
    });
});
