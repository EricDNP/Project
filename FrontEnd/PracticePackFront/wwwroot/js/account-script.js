window.addEventListener('load', init);

function init() {

    const settingsSelector = document.querySelectorAll('.account-settings li[class*="setting"]');
    const setttingsContent = document.querySelectorAll('.setting-content div.setting');
    settingsSelector.forEach(s => s.onclick = () => selectSetting(s));

    const showEditBtns = document.querySelectorAll('.view .operation button:not(.configuration-op)');
    showEditBtns.forEach(b => b.onclick = () => ViewEditControl(b, "show"));

    const showViewBtns = document.querySelectorAll(".edit .operation .btn-cancel");
    showViewBtns.forEach(b => b.onclick = () => ViewEditControl(b, "cancel"));

    function selectSetting(setting) {

        if (!setting.classList.contains("active")) {

            settingsSelector.forEach(s => s.classList.remove("active"));
            selectContent(setting.className);
            setting.classList.add("active");
        }
    }

    function selectContent(settingClass) {

        setttingsContent.forEach(c => {
            c.classList.remove("active");

            if (c.classList.contains(settingClass)) {
                c.classList.add("active");
            }
        });
    }

    function ViewEditControl(btn, action) {
        const parent = btn.closest('li[class*="info"]');
        const infos = document.querySelectorAll('li[class*="info"]:not(.configuration)');

        Array.from(infos).forEach(i => {
            const edit = i.querySelector(".edit");
            if (edit.classList.contains("active"))
            {
                ValidateData(edit.closest('li[class*="info"]'));
                edit.classList.remove("active");
            }
        });

        if(action == "show")
            parent.querySelector('.edit').classList.add("active");
    }

    function ValidateData(dataContainer) {
        if (dataContainer.classList.contains("password-info")) {
            dataContainer.querySelector("input#Password").value = "";
            dataContainer.querySelector("input#ConfirmPassword").value = "";
        }
        else if (dataContainer.classList.contains("branch-info")) {
            const value = dataContainer.querySelector("p.data-value");
            const select = dataContainer.querySelector("select#BranchId");

            if (value != null)
                select.value = value.innerText;
            else
                select.value = "";
        }
        else {
            const value = dataContainer.querySelector("p.data-value");
            const input = dataContainer.querySelector("input.form-control");

            if (value != null)
                input.value = value.innerText;
            else
                input.value = "";
        }
    }
}