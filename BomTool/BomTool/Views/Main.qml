import QtQuick 2.9
import QtQuick.Layouts 1.3
import QtQuick.Controls 1.4
import QtQuick.Controls 2.3
import QtQuick.Controls.Material 2.1
import DotNet 1.0

ApplicationWindow {
    id: mainWindow
    width: 960
    height: 720
    visible: true
    title: qsTr("BOM Tool")
    font.family: "Thoma"
    font.pointSize: 10

    // flags: Qt.Window | Qt.FramelessWindowHint
    // Theme settings
    Material.theme: Material.Dark
    Material.accent: '#41cd52'
    Material.primary: '#41cd52'

    // menu
    menuBar: MenuBar {
	    id : menuBar
        Menu {
            title: qsTr("&File")
            Action { text: qsTr("&Open...") }
            MenuSeparator { }
            Action { text: qsTr("&Quit") }
        }

        Menu {
            title: qsTr("&Help")
            Action { text: qsTr("&About") }
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
                    topPadding : 12
                    bottomPadding: 8
                    leftPadding : 8
                    background: Rectangle {
                        color : '#333842'
                        width: contentPane.implicitWidth
                    }
                    
                }
                Repeater {
                    id: repeater
                    Label {
                        lineHeight : 1.2
                        leftPadding : 12
                        topPadding : 8
                        text: modelData
                        background: Rectangle {
                            color : '#2C313A'
                            width: contentPane.implicitWidth
                        }
                        // doubleclick
                        MouseArea {
                             anchors.fill: parent
                             onDoubleClicked: { 
                                 mainContent.text = parent.text 
                             }
                        }
                    }
                    
                }
            }
        }
    
        // right place holder
      
            
            // Text {
            //     id : mainContent
            //     anchors.centerIn: parent
            //     text: parent.width + 'x' + parent.height
            //     color: 'white'
            //     font.pointSize : 10
            //     font.weight : Font.Bold
            // }
        Pane {
            width : mainLayout.width - leftPane.width
            id : rightPane
            background : Rectangle {
                color : '#2C313A'
                width: rightPane.width
            }
        Column {
            StackLayout{}
            
            ScrollView {}
            Row{
                height : 60
                Button{ text: "Ok"}
            }
        }}
        // modify splitter style
        handleDelegate: Rectangle {
            width: 5
            color: Material.dialogColor
        }
    }


   // Model
   FileListViewModel {
       id : fileVM
       Component.onCompleted: {
           fileVM.makeTestData()
           repeater.model = Net.toListModel(fileVM.paths)
        }
    }

}



