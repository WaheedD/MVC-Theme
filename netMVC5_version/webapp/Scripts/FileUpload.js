var prefix = "#";
var imagePath = '/uploads/images/';

function FileUploadNew(prefix, name, isPdf) {

    var divId = '#dv' + name;

    $(prefix + name).val('');
    $(divId + '.fileupload.fileupload-exists').prop('class', 'fileupload fileupload-new');

    if (isPdf) {
        $(divId + ' .fileupload-preview').html('');
    }
    else {
        $(divId + ' .fileupload-preview.fileupload-exists.thumbnail').html('');
    }
}

function FileUploadExists(prefix, name, fileName, isPdf) {
    
    var divId = '#dv' + name;

    $(prefix + name).val(fileName);
    $(divId + '.fileupload.fileupload-new').prop('class', 'fileupload fileupload-exists');

    if (isPdf) {
        $(divId + ' .fileupload-preview').html(fileName);
    }
    else {
        $(divId + ' .fileupload-preview.fileupload-exists.thumbnail').html('<img src="' + fileName + '">');
    }
}

function FileUpload(prefix, name, fileName, isPdf) {

    if (fileName == null || fileName == '') {
        FileUploadNew(prefix, name, isPdf);
    }
    else {
        FileUploadExists(prefix, name, fileName, isPdf);
    }
}

function BindFileUpload(prefix, name, isPdf) {

    var url = '/Home/UploadFile/';

    if (isPdf) {
        url += '?pdf=true';
    }

    $('#' + name + 'File').fileupload({
        dataType: 'json',
        url: url,
        //acceptFileTypes: /(\.|\/)(jpe?g|png)$/i,
        autoUpload: true,
        done: function (e, data) {
            
            FileUpload(prefix, name, data.result.fileName, isPdf);
        }
    });
}

function BindFileRemove(prefix, name, isPdf) {

    $('#' + name + '_remove').click(function () {
        FileUploadNew(prefix, name, isPdf);
    });
}