﻿$(document).ready(function () {
    initNavbar();
    onDeleteButtonClick();
});

function initNavbar() {
    // Check for click events on the navbar burger icon
    $('.navbar-burger').click(function () {
        // Toggle the 'is-active' class on both the 'navbar-burger' and the 'navbar-menu'
        $('.navbar-burger').toggleClass('is-active');
        $('.navbar-menu').toggleClass('is-active');
    });
}

function onDeleteButtonClick() {
    $('.notification .delete').click(function () {
        var panel = $(this).parent();
        panel.remove();
    });
}

function onCheckboxChanged(checkbox) {
    var isChecked = checkbox.checked;
    var container = $(checkbox).closest('.media');
    var boxChecked = $(container).find('.objective-checkbox .icon.icon-checked');
    var boxUnchecked = $(container).find('.objective-checkbox .icon.icon-unchecked');
    var description = $(container).find('.objective-description');
    if (isChecked) {
        boxChecked.removeClass('is-hidden');
        boxUnchecked.addClass('is-hidden');
        description.addClass('is-checked');
    }
    else {
        boxChecked.addClass('is-hidden');
        boxUnchecked.removeClass('is-hidden');
        description.removeClass('is-checked');
    }
    $(checkbox).siblings('input[type=submit]').trigger('click');
}

function onClickCheckboxIcon(icon) {
    var checkbox = $(icon).siblings('input:checkbox:first');
    checkbox.trigger('click');
}

function openModal(editIcon) {
    var container = $(editIcon).closest('.modal-container');
    var modal = container.find('.modal:first');
    modal.addClass('is-active');
}

$('.modal-close').click(closeModal);

function closeModal() {
    var modal = $(this).closest('.modal');
    modal.removeClass('is-active');
}

function removeClass(obj, removeClass) {
    var objClass = obj.attr('class').replace(removeClass, '');
    obj.removeClass();
    obj.addClass(objClass);
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

function onClickObjectiveDescription(description) {
    var container = $(description).closest('.objective-container');
    container.addClass('edit');
}

function onClickSaveObjectiveDescription(saveButton) {
    var container = $(saveButton).closest('.objective-container');
    container.removeClass('edit');
}