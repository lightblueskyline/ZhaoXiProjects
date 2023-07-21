// 菜单列表模型
export default interface TreeModel {
    ID: string
    Index: string
    Name: string
    Icon: string
    FilePath: string
    Children: Array<TreeModel>
}