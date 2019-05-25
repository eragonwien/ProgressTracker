$(document).ready(function () {
   initMaterialize();
   //focusFirstField();
});

function initMaterialize() {
   $('.sidenav').sidenav();
   $('.collapsible').collapsible();
   $('.modal').modal();
   M.updateTextFields();
   loadToast();
}

function focusFirstField() {
   var form = $('form').first();
   form.focus();
}

function loadToast() {
   var toaster = $('#toast');
   if (toaster && toaster.length > 0) {
      M.toast({ html: toaster.html(), classes: 'rounded' });
   }
}

function onCheckboxChanged(checkbox) {
   $(checkbox).closest('form').find('#submit-checkbox-change-button').trigger('click');
   $(checkbox).closest('form').find('label span').toggleClass('line-through', $(checkbox).is(':checked'));
}

function reloadPage() {
   location.reload();
}

function setAntiForgeryToken(xhr) {
   xhr.setRequestHeader('XSRF-TOKEN', $("input:hidden[name='__RequestVerificationToken']").val());
}

$('.ajax-submit').click(function (e) {
   e.preventDefault();
   var form = $(this).closest('form');
   var url = $(this).attr('formaction');
   ajax('post', url, form.serialize());
});

function ajaxSubmit(button, onSuccess, onError, onDone) {
   var form = $(button).closest('form');
   var url = $(button).attr('formaction');
   ajax('post', url, form.serialize(), onSuccess, onError, onDone);
}

function ajax(type, url, data, onSuccess, onError, onDone) {
   $.ajax({
      type: type,
      url: url,
      data: data,
      beforeSend: setAntiForgeryToken,
      success: onSuccess,
      error: onError
   }).done(onDone);
}