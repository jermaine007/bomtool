import QtQuick 2.9
import QtQuick.Layouts 1.3
import QtQuick.Controls 1.4
import QtQuick.Controls 2.3
import QtQuick.Controls.Material 2.1
import QtQuick.Dialogs 1.3
import QtQuick.Controls.Styles 1.4


// right place holder
ScrollView {

    property var dataGrid: gridView

    id: rightPane
    leftPadding: 10
    topPadding: 10
    ScrollBar.horizontal.policy: ScrollBar.AlwaysOn
    ScrollBar.vertical.policy: ScrollBar.AsNeeded
    contentWidth: readContent.implicitWidth
    contentHeight: readContent.implicitHeight
    Layout.minimumWidth: 680
    
    Column {
        id: readContent
        Repeater {
            id: gridView

            Row {
                property var __lines: Net.toListModel(modelData.lines)
                Column {
                    Label {
                        padding: 10
                        width: 80

                        background: Rectangle {
                            color: '#2C313A'
                            border.color: "gray"
                            border.width: 1
                        }

                        text: modelData.reference
                    }
                }

                Column {

                    Label {
                        padding: 10
                        width: 120
                        background: Rectangle {
                            color: '#2C313A'
                            border.color: "gray"
                            border.width: 1
                        }
                        text: modelData.code
                    }
                }

                Column {

                    Label {
                        padding: 10
                        width: 80
                        background: Rectangle {
                            color: '#2C313A'
                            border.color: "gray"
                            border.width: 1
                        }
                        text: modelData.type
                    }
                }

                Column {

                    Label {
                        padding: 10
                        width: 240
                        background: Rectangle {
                            color: '#2C313A'
                            border.color: "gray"
                            border.width: 1
                        }
                        text: modelData.description
                    }
                }

                Column {

                    Label {
                        padding: 10
                        width: 160
                        background: Rectangle {
                            color: '#2C313A'
                            border.color: "gray"
                            border.width: 1
                        }
                        text: modelData.value
                    }
                }

                Repeater {
                    id: subView
                    Column {
                        Label {
                            padding: 10
                            width: 120
                            background: Rectangle {
                                color: '#2C313A'
                                border.color: "gray"
                                border.width: 1
                            }
                            text: modelData
                        }
                    }
                    Component.onCompleted : {
                        subView.model = __lines
                    }
                }
            }
        }

    }
    background: Rectangle {
        color: '#2C313A'
        width: rightPane.width
    }
}
        // modify splitter style
