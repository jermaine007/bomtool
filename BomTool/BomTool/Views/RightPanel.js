var view;
var callback;
var log

function createReadPanel() {
    __createPanel(true)
}

function createWritePanel() { 
    __createPanel(false)
}

function __createPanel(isRead) {
    var qml = isRead ? 'ReadDataPanel.qml' : 'WriteDataPanel.qml';
    log('qml file: ' + qml)
    view = Qt.createComponent(qml);
    if (view.status == Component.Ready) {
        log('Creation completed')
        __finishCreation();
    }
    else {
        view.statusChanged.connect(__finishCreation);
    }
}

function __finishCreation() {
    callback && callback()
}