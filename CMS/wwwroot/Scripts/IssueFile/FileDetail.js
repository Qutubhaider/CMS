﻿const Role = {
    DeskAdmin: 4,
    DeskOP: 5,
    StoreOP: 6
};


function GetUserDetail() {
    var loData = new Object();
    loData.userId = $('#ddUser').val();
    loadMyRequest(msGetUserDetail, "GET", loData, GetUserDetailSuccess, GetUserDetailError)

}

function GetUserDetailSuccess(fresponse) {
    $('#txtFirstName').val(fresponse.data.stFirstName);
    $('#txtLastName').val(fresponse.data.stLastName);
    $('#txtEmail').val(fresponse.data.stEmail);
    $('#txtMobileA').val(fresponse.data.stMobile);
    $('#txtDepartmentA').val(fresponse.data.stDepartmentName);
    
    if (fresponse.data.inRole == Role.DeskOP) {
        $('#txtUserType').val('Desk Operator');
    }
    else if (fresponse.data.inRole == Role.DeskAdmin) {
        $('#txtUserType').val('Department Admin');
    }
    else if (fresponse.data.inRole == Role.StoreOP) {
        $('#txtUserType').val('Store Operator');
    }

}
function GetUserDetailError() { }

function GetFileDetail() {
    var loData = new Object();
    loData.fileId = $('#StoreId').val();
    loadMyRequest(msGetFileDetail, "GET", loData, GetFileDetailSuccess, GetFileDetailError)

}


function GetFileDetailSuccess(fresponse) {
    $('#txtFileName').val(fresponse.data.stFileName);
    $('#txtEmpoloyeeName').val(fresponse.data.stEmployeeName);
    $('#txtEmployeeNumber').val(fresponse.data.stEmployeeNumber);
    $('#txtPFNumber').val(fresponse.data.stPFNumber);
    $('#txtMobile').val(fresponse.data.stMobile);
    $('#txtPPONumber').val(fresponse.data.stPPONumber);  
}
function GetFileDetailError() { }