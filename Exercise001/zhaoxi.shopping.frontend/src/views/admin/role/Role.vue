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
</template>

<script setup lang="ts">
import { Ref, ref, onMounted } from 'vue';
import RoleModel from '../../../class/RoleModel';

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
};
onMounted(async () => {
    await load();
});
// 查询
const Search = () => { };
// 新增
const AddMenu = () => { };
// 分配菜单
const AssignMenu = () => { };
const tableData: Ref<Array<RoleModel>> = ref<Array<RoleModel>>([]);
const total: Ref<number> = ref<number>(1000);
const handleEdit = (index: number, row: RoleModel) => {
    console.log(index, row);
};
const handleDelete = (index: number, row: RoleModel) => {
    console.log(index, row);
};
</script>

<style scoped lang="scss"></style>