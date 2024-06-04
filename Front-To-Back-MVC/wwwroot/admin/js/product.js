let productImgaeDeleteBtn = document.querySelectorAll(".fa-x");

categoryArchiveBtn.forEach(element => {
    element.addEventListener("click", function () {
        let id = parseInt(this.getAttribute("data-id"));

        (async () => {
            await fetch(`/Product/Restore/${id}`, {
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


