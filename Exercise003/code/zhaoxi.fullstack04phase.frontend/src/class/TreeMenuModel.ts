export default interface TreeMenuModel {
    // 菜单路由地址
    Index: string;
    // 菜单名称
    Name: string;
    // 子菜单 TreeMenuModel[] 类型+方括号
    Children: Array<TreeMenuModel>;
}