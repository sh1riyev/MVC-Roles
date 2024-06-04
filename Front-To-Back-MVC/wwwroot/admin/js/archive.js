let categoryArchiveBtn = document.querySelectorAll(".add-archive");

categoryArchiveBtn.forEach(element => {
    element.addEventListener("click", function () {
        let id = parseInt(this.getAttribute("data-id"));

        (async () => {
            await fetch(`category/settoarchive?id=${id}`, {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
            });
            this.closest(".category-data").remove();
        })();
    });
});
