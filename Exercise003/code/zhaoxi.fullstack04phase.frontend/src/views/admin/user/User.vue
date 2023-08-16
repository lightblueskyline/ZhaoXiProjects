<template>
    <el-card class="box-card">
        <el-row>
            <el-col :span="5">
                <el-input v-model="searchValue" placeholder="请输入搜索值" @change="Search"></el-input>
            </el-col>
            <el-col :span="12">
                <el-button type="primary" @click="Search">查询</el-button>
                <el-button type="primary" @click="open">新增</el-button>
                <el-button>设置角色</el-button>
            </el-col>
        </el-row>
        <br>
        <el-row>
            <el-col>
                <el-table :data="tableData" style="width: 100%;height: 65vh;" border row-key="id">
                    <el-table-column type="index" lable="index" width="70"></el-table-column>
                    <el-table-column prop="date" lable="Date" width="180"></el-table-column>
                    <el-table-column prop="name" label="Name" width="180"></el-table-column>
                    <el-table-column prop="image" label="Image" width="80">
                        <template #defult="scope">
                            <el-image style="vertical-align: middle;" :src="scope.row.image"
                                :preview-src-list="['01.jpeg']"></el-image>
                        </template>
                    </el-table-column>
                    <el-table-column prop="address" label="Address"></el-table-column>
                    <el-table-column label="Operations" align="center">
                        <template #default="scope">
                            <el-button size="small" type="info" @click="handleEdit(scope.$index, scope.row)">编辑</el-button>
                            <el-button size="small" type="danger"
                                @click="handleDelete(scope.$index, scope.row)">删除</el-button>
                        </template>
                    </el-table-column>
                </el-table>
                <el-pagination style="margin-top: 10px;" background layout="prev, pager, next" :total="total" />
            </el-col>
        </el-row>
        <AddUser :isShow="IsShow" :info="info" @closeAdd="closeAdd" @success="success"></AddUser>
    </el-card>
</template>

<script setup lang="ts">
import { Ref, onMounted, ref } from "vue";
import UserResponse from "../../../class/UserResponse";
import AddUser from "./UserAdd.vue";
import { ElMessage } from "element-plus";
import { GetUsers } from "../../../http";

// --- 主画面 START ---
const searchValue = ref("");
const tableData = ref<Array<UserResponse>>([]);
const total = ref(0);
const params = ref({
    Name: "",
    Description: "",
    PageIndex: 1,
    PageSize: 10
});

const load = async () => {
    let response = await GetUsers(params.value) as any;
    tableData.value = response.Data;
    total.value = response.Total;
};
onMounted(async () => {
    await load()
});
const Search = async () => {
    params.value.Name = searchValue.value;
    await load();
};
// --- 主画面 END ---

// --- 变量 ---


const IsShow = ref(false);
const info: Ref<UserResponse> = ref<UserResponse>(new UserResponse());

// --- 方法 ---

const open = () => {
    IsShow.value = true;
};
const closeAdd = () => {
    IsShow.value = false;
    info.value = new UserResponse();
};
const success = async (message: string) => {
    IsShow.value = false;
    info.value = new UserResponse();
    ElMessage.success(message);
};
//
const handleEdit = (index: number, row: {}) => {
    console.log(index, row);
};
//
const handleDelete = (index: number, row: {}) => {
    console.log(index, row);
};
</script>

<style scoped lang="scss"></style>
