mergeInto(LibraryManager.library, {
  OnQuestionShownJS: function(id) {
    console.log("퀴즈 트리거:", id);
  },

  OnCountrySelectedJS: function(countryPtr) {
    var country = UTF8ToString(countryPtr || 0);
    console.log("국가 선택 이벤트(Unity -> Web):", country);

    window.dispatchEvent(
      new CustomEvent("unity:country-selected", {
        detail: {
          country: country
        }
      })
    );
  }
});
