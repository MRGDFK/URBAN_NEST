document.addEventListener("DOMContentLoaded", function () {
    const customSelects = document.querySelectorAll(".custom-select");

    function updateSelectedOptions(customSelect) {
        const selectedOptions = Array.from(customSelect.querySelectorAll(".option.active")).map(function (option) {
            return {
                value: option.getAttribute("data-value"),
                text: option.textContent.trim()
            };
        });

        const selectedValues = selectedOptions.map(function (option) {
            return option.value;
        });

        customSelect.querySelector(".tags_input").value = selectedValues.join(','); // Removed space after comma

        let tagsHTML = "";
        if (selectedOptions.length === 0) {
            tagsHTML = '<span class="placeholder">Select the tags</span>';
        } else {
            const maxTagsToShow = 4;
            let additionalTagsCount = 0;

            selectedOptions.forEach(function (option, index) {
                if (index < maxTagsToShow) {
                    tagsHTML += '<span class="tags">' + option.text + '<span class="remove-tag" data-value="' + option.value + '">x</span></span>';
                } else {
                    additionalTagsCount++;
                }
            });

            if (additionalTagsCount > 0) {
                tagsHTML += '<span class="tags">+' + additionalTagsCount + '</span>';
            }
        }

        customSelect.querySelector(".selected-options").innerHTML = tagsHTML;
    }

    customSelects.forEach(function (customSelect) {
        const searchInput = customSelect.querySelector(".search-tags");
        const optionsContainer = customSelect.querySelector(".options");
        const noResultMessage = customSelect.querySelector(".no-result-message");
        const options = customSelect.querySelectorAll(".option");
        const clearButton = customSelect.querySelector(".clear");

        clearButton.addEventListener("click", function () {
            searchInput.value = "";
            options.forEach(function (option) {
                option.style.display = "block";
            });
            noResultMessage.style.display = "none";
        });

        searchInput.addEventListener("input", function () {
            const searchTerm = searchInput.value.toLowerCase();
            options.forEach(function (option) {
                const optionText = option.textContent.trim().toLowerCase();
                const shouldShow = optionText.includes(searchTerm);
                option.style.display = shouldShow ? "block" : "none";
            });

            const anyOptionsMatch = Array.from(options).some(option => option.style.display === "block");
            noResultMessage.style.display = anyOptionsMatch ? "none" : "block";

            if (searchTerm) {
                optionsContainer.classList.add("option-search-active");
            } else {
                optionsContainer.classList.remove("option-search-active");
            }
        });

        options.forEach(function (option) {
            option.addEventListener("click", function () {
                option.classList.toggle("active");
                updateSelectedOptions(customSelect);
            });
        });
    });

    document.addEventListener("click", function (event) {
        const removeTag = event.target.closest(".remove-tag");
        if (removeTag) {
            const customSelect = removeTag.closest(".custom-select");
            const valueToRemove = removeTag.getAttribute("data-value");
            const optionToRemove = customSelect.querySelector(".option[data-value='" + valueToRemove + "']");
            optionToRemove.classList.remove("active");

            updateSelectedOptions(customSelect);
        }
    });

    const selectBoxes = document.querySelectorAll(".select-box");
    selectBoxes.forEach(function (selectBox) {
        selectBox.addEventListener("click", function (event) {
            if (!event.target.closest(".tags")) {
                selectBox.parentNode.classList.toggle("open");
            }
        });
    });

    document.addEventListener("click", function (event) {
        if (!event.target.closest(".custom-select") && !event.target.classList.contains("remove-tag")) {
            customSelects.forEach(function (customSelect) {
                customSelect.classList.remove("open");
            });
        }
    });

    function resetCustomSelects() {
        customSelects.forEach(function (customSelect) {
            customSelect.querySelectorAll(".option.active").forEach(function (option) {
                option.classList.remove("active");
            });
            updateSelectedOptions(customSelect);
        });
    }

    const submitButton = document.querySelector(".btn_submit");
    submitButton.addEventListener("click", function (event) {
        let valid = true;
        customSelects.forEach(function (customSelect) {
            const selectedOptions = customSelect.querySelectorAll(".option.active");
            const tagErrorMsg = customSelect.querySelector(".tag_error_msg");
            if (selectedOptions.length === 0) {
                tagErrorMsg.textContent = "This field is required";
                tagErrorMsg.style.display = "block";
                valid = false;
            } else {
                tagErrorMsg.textContent = "";
                tagErrorMsg.style.display = "none";
            }
        });

        if (valid) {
            let tags = document.querySelector(".tags_input").value;
            resetCustomSelects();
            customSelects.forEach(customSelect => {
                updateSelectedOptions(customSelect);
            });

            // Allow the form to be submitted
            event.target.form.submit();
        } else {
            event.preventDefault(); // Prevent form submission if validation fails
        }
    });

    // Initialize the selected options display
    customSelects.forEach(customSelect => {
        updateSelectedOptions(customSelect);
    });
});
