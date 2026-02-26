mergeInto(LibraryManager.library, {
  OnQuestionShownJS: function(id) {
    console.log("퀴즈 트리거:", id);
    if(typeof window.onQuestionShownFromUnity === "function") {
      window.onQuestionShownFromUnity(id);
    }
    else {
        console.warn("window.onQuestionShownFromUnity 함수가 정의되어 있지 않습니다.");
    }
  },
  OnAirportLoadedJS : function() {
    console.log("공항 모델 로드 완료 이벤트(Unity -> Web)");
    if(typeof window.onAirportLoadedFromUnity === "function") {
      window.onAirportLoadedFromUnity();
    }
    else {
        console.warn("window.onAirportLoadedFromUnity 함수가 정의되어 있지 않습니다.");
    }
  },
  OnCountrySelectedJS: function(countryPtr) {
    var country = UTF8ToString(countryPtr || 0);
    console.log("국가 선택 이벤트(Unity -> Web):", country);

    if(typeof window.onCountrySelectedFromUnity === "function") {
      window.onCountrySelectedFromUnity(country);
    }
    else {
        console.warn("window.onCountrySelectedFromUnity 함수가 정의되어 있지 않습니다.");
    }
  },
  OnLoadingSceneLoadedJS: function() {
    console.log("씬 로드 이벤트(Unity -> Web)");
    if(typeof window.onSceneLoadedFromUnity === "function") {
      window.onSceneLoadedFromUnity();
    }
    else {
        console.warn("window.onSceneLoadedFromUnity 함수가 정의되어 있지 않습니다.");
    }
  },
});
