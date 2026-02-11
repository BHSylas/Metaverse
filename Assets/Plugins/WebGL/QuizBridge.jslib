
mergeInto(LibraryManager.library, {
  OnQuestionShownJS: function(id) {
    console.log("퀴즈 트리거:", id);
  }
});