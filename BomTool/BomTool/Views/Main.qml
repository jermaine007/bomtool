import QtQuick 2.9
import QtQuick.Layouts 1.3
import QtQuick.Controls 1.4
import QtQuick.Controls 2.3
import QtQuick.Controls.Material 2.1
import QtQuick.Dialogs 1.3 as Dialogs
import QtQuick.Controls.Styles 1.4
import NooneUI 1.0
import "RightPanel.js" as RightPanel

ApplicationWindow {

    property var __rightPanelView: null

    id: mainWindow
    width: 960
    height: 720
    visible: true
    title: qsTr("BOM Tool")
    font.family: "Thoma"
    font.pointSize: 10
    // Theme settings
    Material.theme: Material.Dark
    Material.accent: '#41cd52'
    Material.primary: '#41cd52'

    BusyIndicator {
        id: busyIndicator
        width: 50
        height: 50
        anchors.centerIn: parent
        running: mainVM.isBusy
        z: 1000
    }

    // menu
    menuBar: MenuBar {
        id: menuBar
        Menu {
            title: qsTr("&File")
            Action {
                text: qsTr("&Open...")
                onTriggered: fileDialog.open()
            }
            Action {
                text: qsTr("Generate &BOM...")
                onTriggered: folderDialog.open()
            }
            MenuSeparator { }
            Action {
                text: qsTr("&Clear History")
                onTriggered: {
                    mainVM.clearHistory()
                    repeater.model = Net.toListModel(mainVM.paths)
                }
            }

            MenuSeparator { }
            Action {
                text: qsTr("&Quit")
                onTriggered: Qt.quit()

            }
        }

        Menu {
            title: qsTr("&Help")
            Action { 
                text: qsTr("&About")
                onTriggered : aboutDialog.open() 
            }
        }
    }
    // Main view
    SplitView {
        id: mainLayout
        anchors.fill: parent

        // left view repeater
        ScrollView {
            id: leftPane
            // set scroll bar
            ScrollBar.horizontal.policy: ScrollBar.AsNeeded
            ScrollBar.vertical.policy: ScrollBar.AsNeeded
            // set content area
            contentWidth: contentPane.implicitWidth
            contentHeight: contentPane.implicitHeight

            Layout.minimumWidth: 300
            Layout.maximumWidth: 500

            Column{
                id: contentPane

                Label{
                    text: qsTr("RECENT")
                    font.weight: Font.Bold
                    font.pointSize : 14
                    topPadding: 12
                    bottomPadding: 8
                    leftPadding: 8
                    background: Rectangle {
                        color: '#333842'
                        width: leftPane.width
                    }

                }
                Repeater {
                    id: repeater
                    Label {
                        lineHeight: 1.2
                        leftPadding: 12
                        topPadding: 8
                        text: modelData
                        background: Rectangle {
                            color: '#2C313A'
                            width: contentPane.implicitWidth
                        }
                        // doubleclick
                        MouseArea {
                            anchors.fill: parent
                            onDoubleClicked: {
                                bindReadData(parent.text)
                            }
                        }
                    }

                }
            }

            background: Rectangle {
                color: '#2C313A'
                width: leftPane.width
            }
        }

        Rectangle {
            id: placeholder
            color: '#2C313A'
        }
        
        // modify splitter style
        handleDelegate: Rectangle {
            width: 5
            color: Material.dialogColor
        }
    }

    Rectangle {
        color: Material.dialogColor
        anchors.bottom: parent.bottom
        height: 30
        width: mainLayout.width
        Row {
            Label {
                topPadding: 8
                leftPadding: 8
                id: statusBar
                text: mainVM.statusText
            }
        }
    }


    MainViewModel {
        id: mainVM
        Component.onCompleted : {
            repeater.model = Net.toListModel(mainVM.paths)
        }
    }

    Dialogs.FileDialog {
        id: fileDialog
        title: "Please choose a xls file"
        nameFilters: ["Excel files (*.xls *.xlsx)", "All files (*)"]
        folder: shortcuts.home
        onAccepted: {
            console.log("You chose: " + fileDialog.fileUrl)
            bindReadData(fileDialog.fileUrl)
        }
        onRejected: {
            console.log("Canceled")
        }
        //Component.onCompleted: visible = true
    }

    Dialogs.FileDialog {
        id: folderDialog
        title: "Please choose a folder"
        selectFolder: true
        folder: shortcuts.home
        onAccepted: {
            console.log("You chose: " + folderDialog.fileUrl)
            bindWriteData(folderDialog.fileUrl)
        }
        onRejected: {
            console.log("Canceled")
        }

    }

    Dialog {
       id: aboutDialog
        modal: true
        focus: true
        title: "About"
        anchors.centerIn: parent
        width: 350
        font.pointSize:10
        header: Row {
            Label {
            width: aboutDialog.availableWidth
            text: "BOM Tool"
            horizontalAlignment: Text.AlignHCenter
            topPadding : 10
            font.pointSize: 12
            font.weight: Font.Bold
            }
        }
        Column{
        Label {
            width: aboutDialog.availableWidth
            text: "Information:"
            topPadding :10
            bottomPadding : 10
            font.weight: Font.Bold
        }
           Label {
            width: aboutDialog.availableWidth
            text: "Version: 1.0.0"

        }
        Label {
            width: aboutDialog.availableWidth
            text: "Author: Jermaine"

           
        }
        Label {
            width: aboutDialog.availableWidth
            text: "Platform: .NetCore 3.0.100"

           
        }
        Label {
            width: aboutDialog.availableWidth
            text: "Creadits:"
            topPadding: 10
            bottomPadding : 10
            font.weight: Font.Bold
        }
        Label {
            width: aboutDialog.availableWidth
            textFormat: Text.RichText
            text: "Qml.Net: <a href=\"https://github.com/qmlnet/qmlnet\" style=\"color:white\">https://github.com/qmlnet/qmlnet</a>."
           
            onLinkActivated: Qt.openUrlExternally(link)
        }
         Label {
            width: aboutDialog.availableWidth
            textFormat: Text.RichText
            text: "NPOI.NetCore: <a href=\"https://github.com/dotnetcore/NPOI\" style=\"color:white\">https://github.com/dotnetcore/NPOI</a>."
           
            onLinkActivated: Qt.openUrlExternally(link)
        }
        }
        
        standardButtons: Dialog.Ok
    }

    function bindReadData(path) {
        var task = mainVM.readAsync(path)
        Net.await(task, function (result) {
            RightPanel.callback = function () {

                // remove first view
                if (__rightPanelView) {
                    mainLayout.removeItem(__rightPanelView)
                } else {
                    mainLayout.removeItem(placeholder)
                }

                __rightPanelView = RightPanel.view.createObject()
                mainLayout.addItem(__rightPanelView)

                console.log(mainVM.dataRead.count)
                __rightPanelView.dataGrid.model = Net.toListModel(mainVM.dataRead)
                
                repeater.model = Net.toListModel(mainVM.paths)
                mainVM.isBusy = false
            }

            RightPanel.createReadPanel()

        }, function (error) {
            console.log('Open file failed')
            mainVM.isBusy = false
        })
    }

    function bindWriteData(path) {
        var task = mainVM.writeAsync(path)
        Net.await(task, function (result) {
            RightPanel.callback = function () {

                // remove first view
                if (__rightPanelView) {
                    mainLayout.removeItem(__rightPanelView)
                } else {
                    mainLayout.removeItem(placeholder)
                }

                __rightPanelView = RightPanel.view.createObject()
                mainLayout.addItem(__rightPanelView)

                __rightPanelView.view.model = Net.toListModel(mainVM.grouppedData)
                
                mainVM.isBusy = false
            }
            RightPanel.createWritePanel()

        }, function (result) {
            console.log('Save failed')
            mainVM.isBusy = false
        })
    }

}



