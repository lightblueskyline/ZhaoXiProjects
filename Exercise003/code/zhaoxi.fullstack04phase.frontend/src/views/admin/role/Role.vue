<template>
    <el-card class="box-card">
        <el-row>
            <el-col :span="5">
                <el-input v-model="searchValue" placeholder="请输入搜索值" @change="Search"></el-input>
            </el-col>
            <el-col :span="12">
                <el-button type="primary" @click="Search">查询</el-button>
                <el-button type="primary" @click="openAddEdit">新增</el-button>
                <el-button type="primary" @click="openSettingMenu">分配菜单</el-button>
            </el-col>
        </el-row>
        <br>
        <el-row>
            <el-col>
                <el-table :data="tableData" style="width: 100%;height: 65vh;" border ref="roleTable">
                    <el-table-column type="selection" width="55"></el-table-column>
                    <el-table-column prop="Order" lable="排序" width="80"></el-table-column>
                    <el-table-column prop="Name" label="名称" width="120"></el-table-column>
                    <el-table-column prop="IsEnable" label="是否启用" width="100">
                        <template #defult="scope">
                            <el-switch v-model="scope.row.IsEnable" disabled></el-switch>
                        </template>
                    </el-table-column>
                    <el-table-column prop="Description" label="描述"></el-table-column>
                    <el-table-column prop="CreateUserName" label="创建人" width="80"></el-table-column>
                    <el-table-column prop="CreateDate" label="创建时间"></el-table-column>
                    <el-table-column prop="ModifyUserName" label="修改人" width="80"></el-table-column>
                    <el-table-column prop="ModifyDate" label="修改时间"></el-table-column>
                    <el-table-column prop="IsDeleted" label="是否删除" width="90"></el-table-column>
                    <el-table-column label="操作" align="center">
                        <template #default="scope">
                            <el-button size="small" type="info" @click="handleEdit(scope.$index, scope.row)">编辑</el-button>
                            <el-button size="small" type="danger"
                                @click="handleDelete(scope.$index, scope.row)">删除</el-button>
                        </template>
                    </el-table-column>
                </el-table>
                <el-pagination style="margin-top: 10px;" background layout="prev, pager, next"
                    :total="total"></el-pagination>
            </el-col>
        </el-row>
    </el-card>
    <RoleAddEdit :isShowAddEdit="isShowAddEdit" :modelAddEdit="modelAddEdit" @closeAddEdit="closeAddEdit"
        @successAddEdit="successAddEdit">
    </RoleAddEdit>
    <MenuSetting :isShowSettingMenu="isShowSettingMenu" :roleIds="roleIds" @closeSettingMenu="closeSettingMenu"
        @successSettingMenu="successSettingMenu"></MenuSetting>
</template>

<script setup lang="ts">
import { ref, Ref, onMounted } from "vue";
import { ElMessage, ElTable } from "element-plus";
import RoleModel from "../../../class/RoleModel";
import { GetRoles, DeleteRole } from "../../../http/index";
import RoleAddEdit from "./RoleAddEdit.vue";
import MenuSetting from "./MenuSetting.vue";

// --- 角色页面 START ---
// --- 变量 ---
const searchValue = ref("");
const total = ref(0);
const tableData = ref<Array<RoleModel>>([]);
const params = ref({
    Name: "",
    Description: "",
    PageIndex: 1,
    PageSize: 10
});
const roleTable = ref<InstanceType<typeof ElTable>>();
// --- 方法 ---
const load = async () => {
    let result = await GetRoles(params.value) as any;
    tableData.value = result.Data;
    total.value = result.Total;
};
const Search = async () => {
    params.value.Name = searchValue.value;
    await load();
};
onMounted(async () => {
    await load();
});
/**
 * 编辑角色
 * @param index 
 * @param row 
 */
const handleEdit = (_index: number, row: RoleModel) => {
    modelAddEdit.value = row;
    isShowAddEdit.value = true;
};
/**
 * 删除角色
 * @param index 
 * @param row 
 */
const handleDelete = async (_index: number, row: RoleModel) => {
    await DeleteRole(row.Id);
    await load();
};
// --- 角色页面 END ---

// --- 新增、编辑角色页面 START ---
// --- 变量 ---
const isShowAddEdit = ref(false);
const modelAddEdit: Ref<RoleModel> = ref<RoleModel>(new RoleModel()); // 传入新增、编辑角色页面的模型
// --- 方法 ---
/**
 * 开启新增、编辑角色页面
 */
const openAddEdit = () => {
    modelAddEdit.value = new RoleModel();
    isShowAddEdit.value = true;
};
const closeAddEdit = () => {
    modelAddEdit.value = new RoleModel();
    isShowAddEdit.value = false;
}
const successAddEdit = async (message: string) => {
    modelAddEdit.value = new RoleModel();
    isShowAddEdit.value = false;
    ElMessage.success(message);
    await load();
};
// --- 新增、编辑角色页面 END ---

// --- 分配菜单页面 START ---
// --- 变量 ---
const isShowSettingMenu = ref(false);
const roleIds = ref("");
// --- 方法 ---
const openSettingMenu = () => {
    // 判断是否选中角色
    // 应该只支持单个设置，因为点开界面是存在反选逻辑，如果选择的多个角色它们的菜单不一致，则无法反选
    let rows = roleTable.value?.getSelectionRows();
    if (rows.length > 0) {
        roleIds.value = roleTable.value?.getSelectionRows().map((item: RoleModel) => item.Id).join(",");
        isShowSettingMenu.value = true;
    } else {
        ElMessage.warning("请选择需要分配菜单的角色");
    }
};
const closeSettingMenu = () => {
    isShowSettingMenu.value = false;
};
const successSettingMenu = () => {
    isShowSettingMenu.value = false;
};
// --- 分配菜单页面 END ---
</script>

<style scoped lang="scss"></style>
