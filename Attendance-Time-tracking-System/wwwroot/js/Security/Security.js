window.addEventListener("load", function () {


    const body = document.querySelector('body'),
        sidebar = body.querySelector('nav'),
        toggle = body.querySelector(".toggle"),
        modeSwitch = body.querySelector(".toggle-switch"),
        modeText = body.querySelector(".mode-text");
    tablebody = body.querySelector("table tbody tr");

    toggle.addEventListener("click", () => {
        sidebar.classList.toggle("close");
    })

    modeSwitch.addEventListener("click", () => {
        body.classList.toggle("dark");

        if (body.classList.contains("dark")) {
            modeText.innerText = "Light mode";
        } else {
            modeText.innerText = "Dark mode";
            tablebody.style.backgroundColor = "transparent";
        }
    });

});