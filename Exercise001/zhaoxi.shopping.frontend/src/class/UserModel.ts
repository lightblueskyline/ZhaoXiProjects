export default class UserModel {
    ID: string = ""
    Name: string = ""
    NickName: string = ""
    Password: string = ""
    RoleName: string = ""
    Order: number = 0
    IsEnable: boolean = false
    Description: string = ""
    CreateUserName: string = ""
    CreateDate: Date = new Date()
    ModifyUserName: string = ""
    ModifyDate: Date = new Date()
    IsDeleted: number = 0
}