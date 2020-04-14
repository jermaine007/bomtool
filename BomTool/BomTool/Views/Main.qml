import QtQuick 2.9
import QtQuick.Layouts 1.3
import QtQuick.Controls 1.4
import QtQuick.Controls 2.3
import QtQuick.Controls.Material 2.1
import QtQuick.Dialogs 1.3
import QtQuick.Controls.Styles 1.4
import NooneUI 1.0

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
	    id : menuBar
        Menu {
            title: qsTr("&File")
            Action { 
              text: qsTr("&Open...")
              onTriggered: fileDialog.open()
            }
            Action {
                text: qsTr("&Generate BOM...")
                onTriggered: folderDialog.open()
            }
            MenuSeparator { }
            Action {
                text : qsTr("&Clear History")
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
                        width: leftPane.width
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
                                 bindData(parent.text)
                             }
                        }
                    }
                    
                }
            }

            background : Rectangle {
               color : '#2C313A'
               width: leftPane.width
            }
        }
    
        // right place holder
        ScrollView {
            id : rightPane
            leftPadding :10
            topPadding :10
            ScrollBar.horizontal.policy: ScrollBar.AsNeeded
            ScrollBar.vertical.policy: ScrollBar.AsNeeded
            contentWidth: readContent.implicitWidth
		    contentHeight: readContent.implicitHeight
            
            
            Column {
                id : readContent
                Repeater {
                    id: gridView
                    Row {
                        Column {
                            Label {
				        	    padding : 10
                                width: 80
				        		background : Rectangle {
				        		  color : '#2C313A'
				        		  border.color: "gray"
				        		  border.width: 1
				        		}
                                text :  modelData.reference
                            }
                        }
                        Column {
				           
                            Label {
                                padding : 10
                                width: 120
				        		background : Rectangle {
				        		  color : '#2C313A'
				        		  border.color: "gray"
				        		  border.width: 1
				        		}
                                text : modelData.code
                            }
                        }
                        Column {
				           
                            Label {
				        	    padding : 10
                                width: 80
				        		background : Rectangle {
				        		  color : '#2C313A'
				        		  border.color: "gray"
				        		  border.width: 1
				        		}
                                text :  modelData.type
                            }
                        }
                        Column {
				           
                            Label {
				        	    padding : 10
                                width: 240
				        		background : Rectangle {
				        		  color : '#2C313A'
				        		  border.color: "gray"
				        		  border.width: 1
				        		}
                                text :  modelData.description
                            }
                        }
                        Column {
				            
                            Label {
                                padding : 10
                                width: 160
				        		background : Rectangle {
				        		  color : '#2C313A'
				        		  border.color: "gray"
				        		  border.width: 1
				        		}
                                text :  modelData.value
                            }
                        }
                        Column {
				            
                            Label {
                                padding : 10
                                width: 125
				        		background : Rectangle {
				        		  color : '#2C313A'
				        		  border.color: "gray"
				        		  border.width: 1
				        		}
                                text :  modelData.pth
                            }
                        }
                        Column {
				            
                            Label {
                                padding : 10
                                width: 125
				        		background : Rectangle {
				        		  color : '#2C313A'
				        		  border.color: "gray"
				        		  border.width: 1
				        		}
                                text :  modelData.smd
                            }
                        }
                    }
                }
                
            }

            Column {
                id : writeContent
                visible : false
                Label {
                    padding :10
                    text : qsTr("PTH")
                    font.weight: Font.Bold
                    font.pointSize : 14
                }
                Repeater {
                    id: pthView
                    Row {
                        
                        Column {
				           
                            Label {
                                padding : 10
                                width: 120
				        		background : Rectangle {
				        		  color : '#2C313A'
				        		  border.color: "gray"
				        		  border.width: 1
				        		}
                                text : modelData.code
                            }
                        }
                        Column {
				           
                            Label {
				        	    padding : 10
                                width: 80
				        		background : Rectangle {
				        		  color : '#2C313A'
				        		  border.color: "gray"
				        		  border.width: 1
				        		}
                                text :  modelData.type
                            }
                        }
                        Column {
				           
                            Label {
				        	    padding : 10
                                width: 240
				        		background : Rectangle {
				        		  color : '#2C313A'
				        		  border.color: "gray"
				        		  border.width: 1
				        		}
                                text :  modelData.description
                            }
                        }
                        Column {
				            
                            Label {
                                padding : 10
                                width: 160
				        		background : Rectangle {
				        		  color : '#2C313A'
				        		  border.color: "gray"
				        		  border.width: 1
				        		}
                                text :  modelData.value
                            }
                        }

                        Column {
                            Label {
				        	    padding : 10
                                width: 250
				        		background : Rectangle {
				        		  color : '#2C313A'
				        		  border.color: "gray"
				        		  border.width: 1
				        		}
                                text :  modelData.reference
                            }
                        }
                    }
                }
                Label {
                    padding :10
                    text : qsTr("SMD")
                    font.weight: Font.Bold
                    font.pointSize : 14
                }
                Repeater {
                    id: smdView
                    Row {
                        
                        Column {
				           
                            Label {
                                padding : 10
                                width: 120
				        		background : Rectangle {
				        		  color : '#2C313A'
				        		  border.color: "gray"
				        		  border.width: 1
				        		}
                                text : modelData.code
                            }
                        }
                        Column {
				           
                            Label {
				        	    padding : 10
                                width: 80
				        		background : Rectangle {
				        		  color : '#2C313A'
				        		  border.color: "gray"
				        		  border.width: 1
				        		}
                                text :  modelData.type
                            }
                        }
                        Column {
				           
                            Label {
				        	    padding : 10
                                width: 240
				        		background : Rectangle {
				        		  color : '#2C313A'
				        		  border.color: "gray"
				        		  border.width: 1
				        		}
                                text :  modelData.description
                            }
                        }
                        Column {
				            
                            Label {
                                padding : 10
                                width: 160
				        		background : Rectangle {
				        		  color : '#2C313A'
				        		  border.color: "gray"
				        		  border.width: 1
				        		}
                                text :  modelData.value
                            }
                        }

                        Column {
                            Label {
				        	    padding : 10
                                width: 250
				        		background : Rectangle {
				        		  color : '#2C313A'
				        		  border.color: "gray"
				        		  border.width: 1
				        		}
                                text :  modelData.reference
                            }
                        }
                    }
                }
            }
            background : Rectangle {
               color : '#2C313A'
               width: rightPane.width
            }
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
                topPadding : 8
                leftPadding : 8
                id: statusBar
                text : mainVM.statusText
            }
        }
    }


    MainViewModel {
        id: mainVM
        Component.onCompleted : {
            repeater.model = Net.toListModel(mainVM.paths)
        }
    }

    FileDialog {
        id: fileDialog
        title: "Please choose a xls file"
        nameFilters: [ "Excel files (*.xls *.xlsx)", "All files (*)" ]
        folder: shortcuts.home
        onAccepted: {
            console.log("You chose: " + fileDialog.fileUrl)
            bindData(fileDialog.fileUrl)
        }
        onRejected: {
            console.log("Canceled")
        }
        //Component.onCompleted: visible = true
    }

    FileDialog {
        id: folderDialog
        title: "Please choose a folder"
        selectFolder : true
        folder: shortcuts.home
        onAccepted: {
            console.log("You chose: " + folderDialog.fileUrl)
            var task = mainVM.writeAsync(folderDialog.fileUrl)
            Net.await(task, function(result) {
               if(mainVM.pthData.length != 0 
                 || mainVM.smdData.length !=0){
                    showView(false)
                    pthView.model = Net.toListModel(mainVM.pthData)
                    smdView.model = Net.toListModel(mainVM.smdData)
               }
               mainVM.isBusy = false
            }, function(result) {
               console.log('Save failed')
               mainVM.isBusy = false
            })
        }
        onRejected: {
            console.log("Canceled")
        }
        
    }

    function bindData(path){
        
        var task = mainVM.readAsync(path)
        Net.await(task, function(result) {
               showView(true)
               gridView.model = Net.toListModel(mainVM.dataRead)
               repeater.model = Net.toListModel(mainVM.paths)
               mainVM.isBusy = false
               
        }, function(error){
            console.log('Open file failed')
            mainVM.isBusy = false
        })
    }

    function showView(isRead) {
        readContent.visible = isRead 
        writeContent.visible = !isRead
    }

}



