<template>
    <div>
        <h1>Users</h1>

        <div v-if="pagedListOfUserListDto.totalCount">
            <table class="table">
                <thead>
                    <tr>
                        <th>Actions</th>
                        <th>User Name</th>
                        <th>E-Mail</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="item in pagedListOfUserListDto.items" :key="item.id">
                        <td>
                            <div class="btn-group">
                                <button v-if="!isAdminUser(item.userName)"
                                        v-on:click="remove(item.id)"
                                        class="btn btn-danger btn-sm">
                                    <i class="fas fa-trash-alt"></i>
                                </button>
                            </div>
                        </td>
                        <td>{{ item.userName }}</td>
                        <td>{{ item.email }}</td>
                    </tr>
                </tbody>
            </table>
            <b-pagination align="right"
                          :total-rows="pagedListOfUserListDto.totalCount"
                          v-model="currentPage"
                          :per-page="10"
                          @input="changePage">
            </b-pagination>
        </div>

        <p v-else><em>Loading...</em></p>
    </div>
</template>

<script src="./user-list.ts"></script>
