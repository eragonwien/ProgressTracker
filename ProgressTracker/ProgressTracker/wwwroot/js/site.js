$(document).ready(function () {
   initMaterialize();
});

function initMaterialize() {
   $('.sidenav').sidenav();
   $('.collapsible').collapsible();
   loadModal();
   $('.dropdown-trigger').dropdown();
   M.updateTextFields();
   loadToast();
}

function loadModal() {
   var elems = document.querySelectorAll('.modal');
   M.Modal.init(elems, { onOpenEnd: onModalOpen });

   function onModalOpen(modal, trigger) {
      $(modal).find('form:first .input-field .focus-first').focus();
   }
}

function loadToast() {
   var toaster = $('#toast');
   if (toaster && toaster.length > 0) {
      M.toast({ html: toaster.html(), classes: 'rounded' });
   }
}

function onCheckboxChanged(checkbox) {
   var data = {
      id: $(checkbox).data('id'),
      completed: $(checkbox).is(':checked')
   };
   ajax('POST', $(checkbox).data('url'), data);
   $(checkbox).siblings('.checkbox-label').toggleClass('line-through', $(checkbox).is(':checked'));
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