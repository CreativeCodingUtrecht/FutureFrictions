<script lang="ts">
	import { getDrawerStore } from '@skeletonlabs/skeleton';

	const drawerStore = getDrawerStore();

	function drawerClose(): void {
		drawerStore.close();
	}
</script>

<nav class="list-nav p-4">
	<ul>
		<!-- <li><a href="/" on:click={drawerClose}>Hello Future</a></li> -->
        <li><a href="/scenarios" on:click={drawerClose}>Scenarios</a></li>
	</ul>
</nav>
