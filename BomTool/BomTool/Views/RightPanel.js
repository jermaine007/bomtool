var view;
var callback;

function createReadPanel() {
    __createPanel(true)
}

function createWritePanel() { 
    __createPanel(false)
}

function __createPanel(isRead) {
    var qml = isRead ? 'ReadDataPanel.qml' : 'WriteDataPanel.qml';
    console.log(qml)
    view = Qt.createComponent(qml);
    if (view.status == Component.Ready) {
        console.log('creation completed')
        __finishCreation();
    }
    else {
        view.statusChanged.connect(__finishCreation);
    }
}

function __finishCreation() {
    callback && callback()
}