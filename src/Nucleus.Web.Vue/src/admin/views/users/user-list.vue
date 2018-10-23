<template>
    <div>
        <h1>Users</h1>

        <table v-if="pagedListOfUserListDto.totalCount" class="table">
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
                            <button v-if="item.userName != 'admin'"
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

        <p v-else><em>Loading...</em></p>
    </div>
</template>

<script src="./user-list.ts"></script>
