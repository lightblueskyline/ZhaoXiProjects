<template>
    <el-card class="box-card">
        <el-row>
            <el-col :span="5">
                <el-input v-model="searchValue" placeholder="请输入搜索值" @change="Search"></el-input>
            </el-col>
            <el-col :span="12">
                <el-button type="primary" @click="Search">查询</el-button>
                <el-button type="primary" @click="openAddMenu">新增</el-button>
            </el-col>
        </el-row>
        <br>
        <el-row>
            <el-col>
                <el-table :data="tableData" :tree-props="{ children: 'Children' }" style="width: 100%;height: 65vh;" border
                    row-key="Id">
                    <el-table-column prop="Order" lable="排序" width="80"></el-table-column>
                    <el-table-column prop="Name" lable="名称" width="180"></el-table-column>
                    <el-table-column prop="Index" label="路径" width="80"></el-table-column>
                    <el-table-column prop="Icon" label="图标" width="80">
                        <template #defult="scope">
                            <IconComp :iconname="scope.row.icon"></IconComp>
                        </template>
                    </el-table-column>
                    <el-table-column prop="FilePath" label="组件名称" width="180"></el-table-column>
                    <el-table-column prop="IsEnable" label="是否启用" width="100">
                        <template #defult="scope">
                            <el-switch v-model="scope.row.IsEnable" disabled></el-switch>
                        </template>
                    </el-table-column>
                    <el-table-column prop="Description" label="描述"></el-table-column>
                    <el-table-column label="Operations" align="center">
                        <template #default="scope">
                            <el-button size="small" type="info" @click="handleEdit(scope.$index, scope.row)">编辑</el-button>
                            <el-button size="small" type="danger"
                                @click="handleDelete(scope.$index, scope.row)">删除</el-button>
                        </template>
                    </el-table-column>
                </el-table>
            </el-col>
        </el-row>
    </el-card>
    <AddMenuVue :isShow="isShowDialog" :info="initAddMenu" @closeAddMenu="closeAddMenu" @success="success"></AddMenuVue>
</template>

<script setup lang="ts">
import { ElMessage } from "element-plus";
import { onMounted, Ref, ref } from "vue";
import IconComp from "../../../components/IconComp.vue";
import MenuModel from "../../../class/MenuModel";
import { GetMenus, DeleteMenu } from "../../../http/index";
import { SettingUserDynamicRouter } from "../../../tool/index";
import AddMenuVue from "./AddMenu.vue";
// --- 变量 ---
let params = {
    Name: "",
    Index: "",
    FilePath: "",
    Description: ""
};
const searchValue = ref("");
const tableData: Ref<Array<MenuModel>> = ref<Array<MenuModel>>([]);
const isShowDialog = ref(false); // 是否显示新增菜单弹窗
const initAddMenu: Ref<MenuModel> = ref<MenuModel>(new MenuModel());
// --- 方法 ---
const load = async () => {
    params.Name = searchValue.value;
    tableData.value = await GetMenus(params) as any as Array<MenuModel>;
}
// 搜索方法
const Search = async () => {
    await load();
};
// 页面加载完成之后，载入数据
onMounted(load);
// 编辑方法
const handleEdit = (index: number, row: MenuModel) => {
    console.log(index);
    initAddMenu.value = row;
    isShowDialog.value = true;
};
// 删除方法
const handleDelete = async (index: number, row: MenuModel) => {
    console.log(index);
    await DeleteMenu(row.Id);
    await load();
};
// 开启新增菜单弹窗
const openAddMenu = () => {
    isShowDialog.value = true;
};
// 关闭新增菜单弹窗
const closeAddMenu = () => {
    isShowDialog.value = false;
    initAddMenu.value = new MenuModel();
};
const success = async (message: string) => {
    isShowDialog.value = false;
    initAddMenu.value = new MenuModel;
    ElMessage.success(message);
    // 重载路由
    await SettingUserDynamicRouter();
};
</script>

<style scoped lang="scss"></style>
