export default class MenuModel {
    Id: string = ""
    Name: string = ""
    Index: string = ""
    FilePath: string = ""
    ParentId: string = ""
    Order: number = 0
    IsEnable: boolean = true
    Icon: string = ""
    Children: Array<MenuModel> = []
}