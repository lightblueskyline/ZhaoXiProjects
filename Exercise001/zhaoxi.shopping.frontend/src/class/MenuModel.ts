export default class MenuModel {
    ID: string = ""
    Name: string = ""
    Index: string = ""
    FilePath: string = ""
    ParentID: string = ""
    Order: number = 0
    IsEnable: boolean = true
    Icon: string = ""
    Description: string = ""
    Children: Array<MenuModel> = []
}