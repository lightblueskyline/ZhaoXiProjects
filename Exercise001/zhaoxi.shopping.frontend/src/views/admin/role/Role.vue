<template>
    <el-card class="box-card">
        <el-row>
            <el-col :span="5">
                <el-input v-model="searchVal" placeholder="Please Input" @change="Search" />
            </el-col>
            <el-col :span="12">
                <el-button type="primary" @click="Search">查询</el-button>
                <el-button type="primary" @click="AddMenu">新增</el-button>
                <el-button type="primary" @click="AssignMenu">分配菜单</el-button>
            </el-col>
        </el-row>
        <br>
        <el-row>
            <el-col>
                <el-table :data="tableData" style="width: 100%;height: 65vh;" border ref="tb">
                    <el-table-column prop="Selection" width="55"></el-table-column>
                    <el-table-column prop="Order" label="排序" width="80"></el-table-column>
                    <el-table-column prop="Name" label="名称" width="120"></el-table-column>
                    <el-table-column prop="IsEnable" label="是否启用" width="100">
                        <template #default="scope">
                            <el-switch v-model="scope.row.IsEnable" disabled></el-switch>
                        </template>
                    </el-table-column>
                    <el-table-column prop="Description" label="描述"></el-table-column>
                    <el-table-column prop="CreateUserName" label="创建人" width="80"></el-table-column>
                    <el-table-column prop="CreateDate" label="创建时间"></el-table-column>
                    <el-table-column prop="ModifyUserName" label="修改人" width="80"></el-table-column>
                    <el-table-column prop="ModifyDate" label="修改时间"></el-table-column>
                    <el-table-column prop="IsDelete" label="是否删除" width="90"></el-table-column>
                    <el-table-column label="操作" align="center">
                        <template #default="scope">
                            <el-button size="small" @click="handleEdit(scope.$Index, scope.row)">Edit</el-button>
                            <el-button size="small" type="danger"
                                @click="handleDelete(scope.$Index, scope.row)">Delete</el-button>
                        </template>
                    </el-table-column>
                </el-table>
                <el-pagination style="margin-top: 10px;" background layout="prev, pager, next"
                    :total="total"></el-pagination>
            </el-col>
        </el-row>
    </el-card>
    <AddRoleVue :isShow="isShow" :info="info" @closeAdd="closeAddRole" @success="successAddRow"></AddRoleVue>
    <SettingRoleVue :isShow="isShowSetting" :roleID="roleID" @closeSettingRole="closeSettingRole"
        @successSettingRole="successSettingRole"></SettingRoleVue>
</template>

<script setup lang="ts">
import { Ref, ref, onMounted } from 'vue';
import RoleModel from '../../../class/RoleModel';
import { DeleteRole, GetRoles } from "../../../http/index";
import AddRoleVue from './AddRole.vue';
import SettingRoleVue from './SettingRole.vue';
import { ElMessage, ElTable } from "element-plus"

const tableData: Ref<Array<RoleModel>> = ref<Array<RoleModel>>([]);
const total: Ref<number> = ref<number>(1000);
const searchVal = ref("");
const params = ref({
    Name: "",
    Description: "",
    PageIndex: 1,
    PageSize: 10
});
const load = async () => {
    // 查询数据
    let res = await GetRoles(params.value) as any;
    tableData.value = res.Data;
    total.value = res.total;
};
onMounted(async () => {
    await load();
});
// 查询
const Search = async () => {
    await load();
};
// 新增、修改、删除逻辑 Start
const isShow = ref(false);
// 新增
const AddMenu = () => {
    isShow.value = true;
};
const info: Ref<RoleModel> = ref<RoleModel>(new RoleModel());
const closeAddRole = () => {
    isShow.value = false;
    info.value = new RoleModel();
};
const handleEdit = (index: number, row: RoleModel) => {
    console.log(index, row);
    info.value = row;
    isShow.value = true;
};
const successAddRow = async (message: string) => {
    isShow.value = false;
    info.value = new RoleModel();
    ElMessage.success(message);
    await load();
};
const handleDelete = async (index: number, row: RoleModel) => {
    console.log(index, row);
    await DeleteRole(row.ID);
    await load();
};
// 新增、修改、删除逻辑 End

// 分配菜单逻辑 Start
const tempTB = ref<InstanceType<typeof ElTable>>();
const isShowSetting = ref(false);
const roleID = ref("");
const AssignMenu = () => {
    // 判断是否选中角色
    // 应该只支持单个设置，因为点开界面时存在反选逻辑，如果选择的多个角色它们的菜单不一致，则无法反选
    let tempRows = tempTB.value?.getSelectionRows();
    if (tempRows.length == 1) {
        roleID.value = tempTB.value?.getSelectionRows().map((item: RoleModel) => item.ID).join(",");
        isShowSetting.value = true
    } else if (tempRows.length > 1) {
        ElMessage.warning("清单个选择！");
    } else {
        ElMessage.warning("请选择需要分配菜单的角色！");
    }
};
const closeSettingRole = () => {
    isShowSetting.value = false;
};
const successSettingRole = async () => {
    isShowSetting.value = false;
};
// 分配菜单逻辑 End
</script>

<style scoped lang="scss"></style>