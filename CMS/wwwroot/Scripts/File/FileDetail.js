const Role = {
    DeskAdmin: 4,
    DeskOP: 5,
    StoreOP: 6
};

function GetUserDetail(fiUserId) {
    var loData = new Object();
    loData.userId = fiUserId;
    loadMyRequest(msGetUserDetail, "GET", loData, GetUserDetailSuccess, GetUserDetailError)

}

function GetUserDetailSuccess(fresponse) {
    $('#txtFirstName').val(fresponse.data.stFirstName);
    $('#txtLastName').val(fresponse.data.stLastName);
    $('#txtEmail').val(fresponse.data.stEmail);
    $('#txtMobileA').val(fresponse.data.stMobile);
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

function GetFileDetail(fiFileId) {
    var loData = new Object();
    loData.fileId = fiFileId;
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

