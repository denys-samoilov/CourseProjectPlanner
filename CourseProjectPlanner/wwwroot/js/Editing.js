document.addEventListener("DOMContentLoaded", function () {
	let modal = document.getElementById("myModal");
	if (!modal) return;

	let close = document.getElementById("closeButton");

	let categoryInput = document.getElementById("categoryInput");
	let categoryName = document.getElementById("categoryName");
	let categoryImage = document.getElementById("categoryImage");

	let imageNameOfCategoriesList = window.sharedData.imageNameOfCategoriesList;

	let priceInput = document.getElementById("priceInput");
	let priceOutput = document.getElementById("priceOutput");
	let currencyInput = document.getElementById("currencyInput");

	modal.style.display = "block";

	if (priceInput && priceOutput && currencyInput) {
		priceInput.oninput = function () {
			changeCurrency();
		};

		currencyInput.onchange = function () {
			changeCurrency();
		};

		changeCurrency();
	}

	if (categoryInput) {
		categoryInput.onchange = function () {
			let selectedOption = categoryInput.options[categoryInput.selectedIndex];
			let imgFile = imageNameOfCategoriesList[categoryInput.selectedIndex];

			if (categoryImage && imgFile) {
				categoryImage.src = `/images/${imgFile}`;
			}

			if (categoryName) {
				categoryName.textContent = selectedOption.textContent;
			}
		};
	}

	if (close) {
		close.onclick = function () {
			window.history.go(-1);
		};
	}

	function changeCurrency() {
		const currencySymbols = {
			"UAH": "₴",
			"USD": "$",
			"EUR": "€",
			"GBP": "£",
			"PLN": "zł"
		};

		const selectedCurrency = currencyInput.value;
		const symbol = currencySymbols[selectedCurrency] || "";

		priceOutput.textContent = `${symbol}${priceInput.value}`;
	}
});
