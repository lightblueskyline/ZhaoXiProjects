<template>
    <el-card class="box-card">
        <el-row>
            <el-col :span="5">
                <el-input v-model="searchVal" placeholder="Please Input" @change="Search" />
            </el-col>
            <el-col :span="12">
                <el-button type="primary" @click="Search">查询</el-button>
                <el-button type="primary" @click="AddMenu">新增</el-button>
            </el-col>
        </el-row>
        <br>
        <el-row>
            <el-col>
                <el-table :data="tableData" :tree-props="{ children: 'Children' }" style="width: 100%;height: 65vh;" border
                    row-key="Id">
                    <el-table-column prop="Order" label="排序" width="80"></el-table-column>
                    <el-table-column prop="Name" label="名称" width="180"></el-table-column>
                    <el-table-column prop="Index" label="路径" width="80"></el-table-column>
                    <el-table-column prop="Icon" label="图标" width="80">
                        <template #default="scope">
                            <IconComp :icon="scope.row.Icon"></IconComp>
                        </template>
                    </el-table-column>
                    <el-table-column prop="FilePath" label="组件名" width="180"></el-table-column>
                    <el-table-column prop="IsEnable" label="是否启用" width="100">
                        <template #default="scope">
                            <el-switch v-model="scope.row.IsEnable" disabled></el-switch>
                        </template>
                    </el-table-column>
                    <el-table-column prop="Description" label="描述"></el-table-column>
                    <el-table-column label="Operations" align="center">
                        <template #default="scope">
                            <el-button size="small" @click="handleEdit(scope.$Index, scope.row)">Edit</el-button>
                            <el-button size="small" type="danger"
                                @click="handleDelete(scope.$Index, scope.row)">Delete</el-button>
                        </template>
                    </el-table-column>
                </el-table>
            </el-col>
        </el-row>
    </el-card>
    <AddMenuVue :isShow="isShow" :info="info" @closeAdd="closeAdd" @success="success"></AddMenuVue>
</template>

<script setup lang="ts">
import { Ref, ref, onMounted } from 'vue';
import MenuModel from '../../../class/MenuModel';
import { GetMenus, DeleteMenu } from "../../../http/index";
import AddMenuVue from './AddMenu.vue';
import { ElMessage } from 'element-plus';
import { SettingUserRouter } from '../../../tools/index';

const tableData: Ref<Array<MenuModel>> = ref<Array<MenuModel>>([]);
const searchVal = ref("");
let params = {
    Name: "",
    Index: "",
    FilePath: "",
    Description: ""
};
const load = async () => {
    // 查询数据
    params.Name = searchVal.value;
    tableData.value = await GetMenus(params) as any as Array<MenuModel>;
};
onMounted(async () => {
    await load();
});
// 查询
const Search = () => { };

// 新增、修改、删除逻辑 Start
const isShow = ref(false);
// 新增
const AddMenu = () => {
    isShow.value = true;
};
const info: Ref<MenuModel> = ref<MenuModel>(new MenuModel());
const closeAdd = () => {
    isShow.value = false;
    info.value = new MenuModel();
};
const handleEdit = (index: number, row: MenuModel) => {
    console.log(index, row);
    info.value = row;
    isShow.value = true;
};
const success = async (message: string) => {
    isShow.value = false;
    info.value = new MenuModel();
    ElMessage.success(message);
    await load(); // 重载数据
    // 重载路由
    await SettingUserRouter();
};
const handleDelete = async (index: number, row: MenuModel) => {
    console.log(index, row);
    await DeleteMenu(row.ID);
    await load();
};
// 新增、修改、删除逻辑 End
</script>

<style scoped lang="scss"></style>